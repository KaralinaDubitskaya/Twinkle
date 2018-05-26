﻿using System;
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
        
        public User User { set { UserPanel.DataContext = value; } }

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
    }
}