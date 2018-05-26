using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Twinkle.Views.Dialogs
{
    public class SuccessDialog
    {

        public SuccessDialog(string message)
        {
            MessageBox.Show(message, "Done :P", MessageBoxButton.OK, MessageBoxImage.Information);
        }

    }
}
