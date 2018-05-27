using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Twinkle.Models;
using Tweetinvi.Core.Interfaces;
using Twinkle.Controllers;

namespace Twinkle.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, MainController.IWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public event EventHandler btnTweetClicked;
        public event EventHandler btnHomeClicked;
        public event EventHandler btnProfileClicked;
        public event EventHandler btnSaveClicked;
        public event EventHandler btnLogOutClicked;
        public event EventHandler btnRetweetClicked;
        public event EventHandler btnLikeClicked;
        public event EventHandler btnUserClicked;
        public event EventHandler btnBrowseClicked;
        public event EventHandler btnFollowClicked;

        private Tweets _tweets;
        public Tweets HomeTimeLine
        {
            get
            {
                return _tweets;
            }
            set
            {
                _tweets = value;
                lbTimeline.DataContext = _tweets;
            }
        }
        
        public User User { set { UserPanel.DataContext = value; } get { return (Twinkle.Models.User)UserPanel.DataContext; } }

        public void AddTweet(ITweet tweet)
        {
            _tweets.Insert(0, new Tweet(tweet));
            lbTimeline.Dispatcher.BeginInvoke(new Action(lbTimeline.Items.Refresh));
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            btnProfileClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            btnHomeClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            btnSaveClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btnTweet_Click(object sender, RoutedEventArgs e)
        {
            btnTweetClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            btnLogOutClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btnRetweet_Click(object sender, RoutedEventArgs e)
        {
            btnRetweetClicked?.Invoke(GetButtonParentTweet(sender as Button), EventArgs.Empty);
        }

        private void btnLike_Click(object sender, RoutedEventArgs e)
        {
            btnLikeClicked?.Invoke(GetButtonParentTweet(sender as Button), EventArgs.Empty);
        }

        private Tweet GetButtonParentTweet(Button button)
        {
            return ((((button?.Parent as Grid).Parent as Grid).Children as UIElementCollection)[0] as Label).DataContext as Tweet;
        }

        private void btnUser_Click(object sender, RoutedEventArgs e)
        {
            btnUserClicked?.Invoke(GetButtonParentUser(sender as Button), EventArgs.Empty);
        }

        private User GetButtonParentUser(Button button)
        {
            return ((((button?.Parent as Grid).Children as UIElementCollection)[0] as Label).DataContext as Tweet).User;
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            btnBrowseClicked?.Invoke(GetButtonParentTweet(sender as Button), EventArgs.Empty);
        }

        private void btnFollow_Click(object sender, RoutedEventArgs e)
        {
            if ((string)btnFollow.Content == "Follow")
            {
                btnFollow.Content = "Unfollow";
            }
            else
            {
                btnFollow.Content = "Follow";
            }

            btnFollowClicked?.Invoke(UserPanel.DataContext, EventArgs.Empty);
        }
    }
}
