using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Core.Credentials;
using Twinkle.Auth;
using Twinkle.Models;
using System.Diagnostics;


namespace Twinkle.Controllers
{
    public class LoginController : Controller
    {
        #region interface IWindow
        public interface IWindow
        {
            string Pin { get; }                // return entered pin
            bool RememberChecked { get; }      // true, if it is necessary to remember account
            
            event EventHandler btnAuthorizeClicked;
            event EventHandler btnLoginClicked;

            void Show();
            void Close();
            void ShowError(string message);
            void RepeatAuthorization();
        }
        #endregion

        // Login window
        private IWindow _window;
        public IWindow Window
        {
            get { return _window; }
            set
            {
                _window = value;
                _window.btnAuthorizeClicked += _window_btnAuthorizeClicked;
                _window.btnLoginClicked += _window_btnLoginClicked;
            }
        }

        private void _window_btnAuthorizeClicked(object sender, EventArgs e)
        {
            // Make the user go on the URL so that Twitter authenticates him and gives a PIN code
            Process.Start(CredentialsCreator.GetAuthorizationURL(AppCredentials));
        }

        private void _window_btnLoginClicked(object sender, EventArgs e)
        {
            // With tthe pin code it is now possible to get the credentials back from Twitter
            ITwitterCredentials credentials = CredentialsCreator.GetCredentialsFromVerifierCode
                (Window.Pin, AppCredentials);

            if (credentials != null)
            {
                // Use the user credentials
                Tweetinvi.Auth.SetCredentials(credentials);
            }

            if (User.GetLoggedUser() != null)
            {
                new MainController { Window = new MainWindow() }.HandleNavigation(null);

                if (Window.RememberChecked)
                {
                    // Save user credentials
                    _savedToken.Set(new Token(User.GetLoggedUser().Name, 
                        credentials.AccessToken, credentials.AccessTokenSecret));
                }

                Window.Close();
            }
            else
            {
                Window.ShowError("Incorrect PIN.");
                AppCredentials.AuthorizationKey = null;
                AppCredentials.AuthorizationSecret = null;
                Window.RepeatAuthorization();
            }
        }

        // Was created if the user set check box "Remember me"
        private SavedToken _savedToken;

        public TwitterCredentials AppCredentials { get; set; }

        public override void HandleNavigation(object args)
        {
            _savedToken = new SavedToken();

            // Identify the app
            AppCredentials = new TwitterCredentials("T5rVK7hFUW94mjVKJLI7WERus", 
                "WJMlbomM54nMIdrABZyHfkaepBdkrdGnjJsvwbUo8xOeVXj4iD");


            // The user already was authorized
            if (_savedToken.IsSet)
            {
                // Load saved token from file
                Token token = _savedToken.Load();

                // Set up the application credentials
                TwitterCredentials credentials = new TwitterCredentials(AppCredentials.ConsumerKey, 
                    AppCredentials.ConsumerSecret, token.AccessToken, token.AccessTokenSecret);
                Tweetinvi.Auth.SetCredentials(credentials);

                if (User.GetLoggedUser() != null)
                {
                    new MainController { Window = new MainWindow() }.HandleNavigation(null);
                }
                else
                {
                    Window.ShowError("Connection problem.");
                    Window.Show();
                }
            }
            else
            {
                // The user need to log in
                Window.Show();
            }
        }
    }
}
