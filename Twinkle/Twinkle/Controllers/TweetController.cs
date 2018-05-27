using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Twinkle.Controllers
{
    public class TweetController : Controller
    {
        #region interface IWindow
        public interface IWindow
        {
            string TweetText { get; }  // return entered tweet text
            event EventHandler btnTweetClicked;
            event EventHandler btnBrowseClicked;

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
                _window.btnBrowseClicked += Window_btnBrowseClicked;
            }
        }

        private void Window_btnTweetClicked(object sender, EventArgs e)
        {
            Window.Close();
        }

        private void Window_btnBrowseClicked(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
              //  Window..Text = openFileDialog.SafeFileName;
              //  AddMediaPath = openFileDialog.FileName;
            }
            else
            {

            }
        }

        public override void HandleNavigation(object CreateTweet)
        {
            Window.btnTweetClicked += (EventHandler)CreateTweet;
            Window.ShowDialog();
        }
    }
}
