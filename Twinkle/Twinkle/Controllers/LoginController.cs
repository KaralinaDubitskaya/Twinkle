﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Core.Credentials;
using Twinkle.Auth;
using Twinkle.Models;
using System.Diagnostics;
using Twinkle.Controllers;
using Twinkle.Views;


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
                _window.btnAuthorizeClicked += Window_btnAuthorizeClicked;
                _window.btnLoginClicked += Window_btnLoginClicked;
            }
        }

        private void Window_btnAuthorizeClicked(object sender, EventArgs e)
        {
            // Make the user go on the URL so that Twitter authenticates him and gives a PIN code
            Process.Start(CredentialsCreator.GetAuthorizationURL(AppCredentials));
        }

        private void Window_btnLoginClicked(object sender, EventArgs e)
        {
            // With tthe pin code it is now possible to get the credentials back from Twitter
            ITwitterCredentials credentials = CredentialsCreator.GetCredentialsFromVerifierCode
                (Window.Pin, AppCredentials);

            if (credentials != null)
            {
                // Use the user credentials
                Tweetinvi.Auth.SetCredentials(credentials);
            }

            if (Tweetinvi.User.GetLoggedUser() != null)
            {
                var mainController = new MainController();
                mainController.HandleNavigation(null);

                if (Window.RememberChecked)
                {
                    // Save user credentials
                    SavedToken.Set(new Token(Tweetinvi.User.GetLoggedUser().Name, 
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
        
        public TwitterCredentials AppCredentials { get; set; }

        public override void HandleNavigation(object args)
        {
            // Identify the app
            //AppCredentials = new TwitterCredentials("T5rVK7hFUW94mjVKJLI7WERus", 
            //    "WJMlbomM54nMIdrABZyHfkaepBdkrdGnjJsvwbUo8xOeVXj4iD");
            AppCredentials = new TwitterCredentials("BYOymVcc9Yyk2mA67pLhWxS46", "YSsfV0VmFPEcQqjiZpEvZw28Ngdz4MzO6AEugef5sRzDNGkgmb");


            // The user already was authorized
            if (SavedToken.IsSet)
            {
                // Load saved token from file
                Token token = SavedToken.Load();

                // Set up the application credentials
                TwitterCredentials credentials = new TwitterCredentials(AppCredentials.ConsumerKey, 
                    AppCredentials.ConsumerSecret, token.AccessToken, token.AccessTokenSecret);
                Tweetinvi.Auth.SetCredentials(credentials);

                if (Tweetinvi.User.GetLoggedUser() != null)
                {
                    var mainController = new MainController();
                    mainController.HandleNavigation(null);
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
