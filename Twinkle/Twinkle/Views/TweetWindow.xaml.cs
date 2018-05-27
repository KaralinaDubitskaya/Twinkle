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
using Twinkle.Controllers;
using System.IO;

namespace Twinkle.Views
{
    /// <summary>
    /// Interaction logic for TweetWindow.xaml
    /// </summary>
    public partial class TweetWindow : Window, TweetController.IWindow
    {
        public TweetWindow()
        {
            InitializeComponent();
        }

        private const int MAX_MSG_LEN = 280;
        private const int MAX_FILE_NAME_LEN = 15;
        private string _fileName = "";

        public string TweetText { get { return tbTweet.Text; } }
        public string FileName
        {
            get { return _fileName; }
            set
            {
                _fileName = value;

                var name = System.IO.Path.GetFileName(_fileName);
                if (name.Length > MAX_FILE_NAME_LEN)
                {
                    // Cut the end of the filename
                    name = name.Substring(0, MAX_FILE_NAME_LEN - 3) + "...";
                }
                lblMedia.Content = name;
            }
        }
        public event EventHandler btnTweetClicked;
        public event EventHandler btnBrowseClicked;

        private void btnTweet_Click(object sender, RoutedEventArgs e)
        {
            btnTweetClicked?.Invoke(this, EventArgs.Empty);
        }

        private void tbTweet_TextChanged(object sender, TextChangedEventArgs e)
        {
            lblLeft.Content = MAX_MSG_LEN - tbTweet.Text.Length;
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            btnBrowseClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
