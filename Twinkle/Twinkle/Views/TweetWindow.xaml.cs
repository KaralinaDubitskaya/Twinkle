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

        public string TweetText { get { return tbTweet.Text; } }
        public event EventHandler btnTweetClicked;

        private void btnTweet_Click(object sender, RoutedEventArgs e)
        {
            btnTweetClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
