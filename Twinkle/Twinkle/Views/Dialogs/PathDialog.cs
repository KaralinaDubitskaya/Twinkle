using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Twinkle.Views.Dialogs
{
    public class PathDialog
    {
        public event EventHandler<PathDialogEventArgs> OnPathChanged;

        public void OpenPathDialog()
        {

            SaveFileDialog saveFileDialog;
            saveFileDialog = new SaveFileDialog();

            saveFileDialog.InitialDirectory = @"C:\";

            if (saveFileDialog.ShowDialog() == true)
            {
               OnPathChanged?.Invoke(this, new PathDialogEventArgs { Path = saveFileDialog.FileName });
            }
        }
    }
}
