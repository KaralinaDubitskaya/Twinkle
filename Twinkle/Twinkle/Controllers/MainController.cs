﻿using System;
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

namespace Twinkle.Controllers
{
    public class MainController : Controller
    {
        #region interface IWindow
        public interface IWindow
        {
            event EventHandler btnTweetClicked;
            event EventHandler btnHomeClicked;
            event EventHandler btnProfileClicked;
            event EventHandler btnSaveClicked;
            event EventHandler btnLogOutClicked;

            Tweets HomeTimeLine { get; set; }
            Models.User User { set; }

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
            }
        }

        public override void HandleNavigation(object args)
        {
            Window.HomeTimeLine = new HomeTimeLine().GetTweets();
            Window.User = new Models.User(Tweetinvi.User.GetLoggedUser());
            Window.Show();

            var stream = Tweetinvi.Stream.CreateUserStream();

            stream.TweetCreatedByFriend += (sender, e) =>
            {
                Window.AddTweet(e.Tweet);
            };

            stream.TweetCreatedByMe += (sender, e) =>
            {
                Window.AddTweet(e.Tweet);
            };

            stream.StartStreamAsync();
        }

        private void Window_btnTweetClicked(object sender, EventArgs e)
        {
            new TweetController { Window = new TweetWindow() }.HandleNavigation(TweetCreated);
        }

        private void Window_btnHomeClicked(object sender, EventArgs e)
        {
            Window.HomeTimeLine = new HomeTimeLine().GetTweets();
        }

        private void Window_btnProfileClicked(object sender, EventArgs e)
        {
            Window.HomeTimeLine = new UserTimeLine().GetTweets();
        }

        private void Window_btnSaveClicked(object sender, EventArgs e)
        {

        }

        private void Window_btnLogOutClicked(object sender, EventArgs e)
        {
            Tweetinvi.Auth.SetCredentials(null);
            SavedToken.Delete();

            Window.Close();
            new LoginController { Window = new LoginWindow() }.HandleNavigation(null);
        }

        private void CreateTweet(object sender, EventArgs e)
        {
            try
            {
                var window = sender as TweetWindow;
                string tweet = window.TweetText.Trim();
                Tweetinvi.Tweet.PublishTweet(tweet);
                SuccessDialog successDialog = new SuccessDialog("Your tweet was sent successfully.");
            }
            catch (Exception)
            {
                ErrorDialog errorDialog = new ErrorDialog("Unable to send tweet.");
            }
        }
    }
}