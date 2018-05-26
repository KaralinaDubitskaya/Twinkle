﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Twinkle.Controllers;

namespace Twinkle
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Setup(object sender, StartupEventArgs e)
        {
            LoginController loginController = new LoginController { Window = new LoginWindow() };
            loginController.HandleNavigation(null);
        }
    }
}
