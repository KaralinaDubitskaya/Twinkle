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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Twinkle.Controllers;

namespace Twinkle
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window, LoginController.IWindow
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        public string Pin { get { return tbPin.Text; } }
        public bool RememberChecked { get { return chbRemember.IsChecked.Value; } }

        public event EventHandler btnAuthorizeClicked;
        public event EventHandler btnLoginClicked;

        public void ShowError(string msg)
        {
            MessageBox.Show("Error: " + msg, "Ooooops :(", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void btnAuthorize_Click(object sender, RoutedEventArgs e)
        {
            btnAuthorizeClicked?.Invoke(this, EventArgs.Empty);

            lblLogin.Visibility = Visibility.Hidden;
            lblAuthorize.Visibility = Visibility.Hidden;
            btnAuthorize.Visibility = Visibility.Hidden;
            imgLogo.Visibility = Visibility.Hidden;

            tbPin.Visibility = Visibility.Visible;
            btnLogin.Visibility = Visibility.Visible;
            chbRemember.Visibility = Visibility.Visible;
            lblEnterPin.Visibility = Visibility.Visible;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            btnLoginClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
