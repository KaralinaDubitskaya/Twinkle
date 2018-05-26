using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twinkle.Controllers
{
    public class TweetController : Controller
    {
        #region interface IWindow
        public interface IWindow
        {
            string TweetText { get; }  // return entered tweet text
            event EventHandler btnTweetClicked;

            Nullable<bool> ShowDialog();
            void Close();
        }
        #endregion

        private IWindow _window;
        public IWindow Window
        {
            get { return _window; }
            set
            {
                _window = value;
                _window.btnTweetClicked += Window_btnTweetClicked;
            }
        }

        private void Window_btnTweetClicked(object sender, EventArgs e)
        {
            Window.Close();
        }

        public override void HandleNavigation(object CreateTweet)
        {
            Window.btnTweetClicked += (EventHandler)CreateTweet;
            Window.ShowDialog();
        }
    }
}
