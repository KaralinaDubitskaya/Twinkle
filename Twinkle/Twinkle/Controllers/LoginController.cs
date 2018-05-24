using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;


namespace Twinkle.Controllers
{
    public class LoginController
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
        }
        #endregion


    }
}
