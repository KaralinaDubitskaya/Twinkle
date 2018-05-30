using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinkle.Models;
using Tweetinvi.Core.Interfaces;
using Twinkle.Twitter;
using Tweetinvi;
using Twinkle.Views;
using Twinkle.Views.Dialogs;
using System.Windows;
using Twinkle.Auth;
using System.IO;
using System.Diagnostics;

namespace Twinkle.Controllers
{
    public class MainController : Controller
    {
        #region interface IWindow
        // Main Window
        public interface IWindow
        {
            event EventHandler btnTweetClicked;
            event EventHandler btnHomeClicked;
            event EventHandler btnProfileClicked;
            event EventHandler btnSaveClicked;
            event EventHandler btnLogOutClicked;
            event EventHandler btnRetweetClicked;
            event EventHandler btnLikeClicked;
            event EventHandler btnUserClicked;
            event EventHandler btnBrowseClicked;
            event EventHandler btnFollowClicked;

            Tweets TimeLine { get; set; }
            Models.User User { get; set; }

            void AddTweet(ITweet tweet);
            void Show();
            void Close();
        }
        #endregion

        public MainController()
        {
            Window = new MainWindow();
            TweetCreated += CreateTweet;
        }

        private EventHandler TweetCreated;

        private IWindow _window;
        public IWindow Window
        {
            get { return _window; }
            set
            {
                _window = value;
                _window.btnTweetClicked += Window_btnTweetClicked;
                _window.btnHomeClicked += Window_btnHomeClicked;
                _window.btnProfileClicked += Window_btnProfileClicked;
                _window.btnSaveClicked += Window_btnSaveClicked;
                _window.btnLogOutClicked += Window_btnLogOutClicked;
                _window.btnRetweetClicked += Window_btnRetweetClicked;
                _window.btnLikeClicked += Window_btnLikeClicked;
                _window.btnUserClicked += Window_btnUserClicked;
                _window.btnBrowseClicked += Window_btnBrowseClicked;
                _window.btnFollowClicked += Window_btnFollowClicked;
            }
        }

        public override void HandleNavigation(object args)
        {
            // Show HomeTimeLine
            Window.TimeLine = new HomeTimeLine().GetTweets();
            // Show info about logged user
            var user = new Models.User(Tweetinvi.User.GetLoggedUser());
            // Hide button "Follow"
            user.Admin = "Hidden";

            Window.User = user;
            Window.Show();

            // Check for new tweets
            var stream = Tweetinvi.Stream.CreateUserStream();

            stream.TweetCreatedByFriend += (sender, e) =>
            {
                Window.AddTweet(e.Tweet);
            };

            stream.TweetCreatedByMe += (sender, e) =>
            {
                Window.AddTweet(e.Tweet);
            };

            // Asynchronously update tweets
            stream.StartStreamAsync();
        }

        private void Window_btnTweetClicked(object sender, EventArgs e)
        {
            // Show TweetWindow
            new TweetController { Window = new TweetWindow() }.HandleNavigation(TweetCreated);
        }

        private void Window_btnHomeClicked(object sender, EventArgs e)
        {
            // Show HomeTimeLine
            Window.TimeLine = new HomeTimeLine().GetTweets();
            // Show info about logged user
            var user = new Models.User(Tweetinvi.User.GetLoggedUser());
            // Hide button "Follow"
            user.Admin = "Hidden";
            Window.User = user;
        }

        private void Window_btnProfileClicked(object sender, EventArgs e)
        {
            // Show logged user's timeline
            Window.TimeLine = new UserTimeLine().GetTweets();
            // Show info about logged user
            var user = new Models.User(Tweetinvi.User.GetLoggedUser());
            // Hide button "Follow"
            user.Admin = "Hidden";
            Window.User = user;
        }

        private void Window_btnUserClicked(object sender, EventArgs e)
        {
            // Get selected user timeline
            var user = sender as Models.User;
            Window.TimeLine = new UserTimeLine(user.ID).GetTweets();
            // Show info about selected user
            var usr = new Models.User(Tweetinvi.User.GetUserFromId(user.ID));
            // Show button "(Un)Follow"
            usr.Admin = "Visible";

            // Check if selected user is one of the logged user's followings
            var friends = Tweetinvi.User.GetFriendIds(Tweetinvi.User.GetLoggedUser());
            // Set button content as "Follow" or "Unfollow"
            usr.Follow = (friends.Contains(usr.ID)) ? "Unfollow" : "Follow";

            Window.User = usr;
        }

        private void Window_btnBrowseClicked(object sender, EventArgs e)
        {
            var tweet = sender as Models.Tweet;
            // Open tweet's URL in browser
            Process.Start(tweet.URL);
        }

        private void Window_btnSaveClicked(object sender, EventArgs e)
        {
            // Show SaveWindow
            SaveController saveController = new SaveController { Window = new SaveWindow() };
            saveController.HandleNavigation(null);
        }

        private void Window_btnFollowClicked(object sender, EventArgs e)
        {
            var user = sender as Twinkle.Models.User;
            var loggedUser = Tweetinvi.User.GetLoggedUser();

            var friends = Tweetinvi.User.GetFriendIds(loggedUser);
            if (friends.Contains(user.ID))
            {
                // Unfollow selected user
                loggedUser.UnFollowUserAsync(user.ID);
                user.Follow = "Follow";
            }
            else
            {
                // Follow selected user
                loggedUser.FollowUserAsync(user.ID);
                user.Follow = "Unfollow";
            }

            Window.User = user;
        }

        private void Window_btnRetweetClicked(object sender, EventArgs e)
        {
            var tweet = sender as Models.Tweet;
            // Retweet selected tweet
            if (Tweetinvi.Tweet.PublishRetweet(tweet.ID) != null)
            {
                new SuccessDialog("Reetweet successfully");
            }
        }

        private void Window_btnLikeClicked(object sender, EventArgs e)
        {
            var tweet = sender as Models.Tweet;
            // Like selected tweet
            Tweetinvi.Tweet.FavoriteTweet(tweet.ID);
        }

        private void Window_btnLogOutClicked(object sender, EventArgs e)
        {
            // Unset credentials
            Tweetinvi.Auth.SetCredentials(null);
            // Delete saved token
            SavedToken.Delete();

            Window.Close();
            // Show LoginWindow
            new LoginController { Window = new LoginWindow() }.HandleNavigation(null);
        }

        private void CreateTweet(object sender, EventArgs e)
        {
            try
            {
                var window = sender as TweetWindow;
                string tweet = window.TweetText.Trim();
                var parameters = Tweetinvi.Tweet.CreatePublishTweetOptionalParameters();

                if (window.FileName != "")
                {
                    try
                    {
                        // Load picture
                        byte[] file = File.ReadAllBytes(window.FileName);
                        var image = Upload.UploadImage(file);
                        parameters.Medias = new List<Tweetinvi.Core.Interfaces.DTO.IMedia> { image };
                    }
                    catch (Exception)
                    {
                        ErrorDialog errorDialog = new ErrorDialog("Unable load the file");
                        return;
                    }
                }
                
                Tweetinvi.Tweet.PublishTweet(tweet, parameters);
                SuccessDialog successDialog = new SuccessDialog("Your tweet was sent successfully.");
            }
            catch (Exception)
            {
                ErrorDialog errorDialog = new ErrorDialog("Unable to send tweet.");
            }
        }
    }
}
