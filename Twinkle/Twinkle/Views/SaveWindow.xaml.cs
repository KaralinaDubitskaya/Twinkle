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
    /// Interaction logic for SaveWindow.xaml
    /// </summary>
    public partial class SaveWindow : Window, SaveController.IWindow
    {
        public SaveWindow()
        {
            InitializeComponent();
        }

        public IList<string> ListTypes { set { cbListTypes.DataContext = value; } }

        public string ListTypeSelected
        {
            get
            {
                if (cbTypes.SelectedItem != null)
                {
                    return cbListTypes.SelectedItem.ToString();
                }
                return null;
            }
        }

        public IList<string> ParserNames
        {
            set
            {
                cbTypes.DataContext = value;
            }
        }

        public string ParserSelect
        {
            get
            {
                if (cbTypes.SelectedItem != null)
                {
                    return cbTypes.SelectedItem.ToString();
                }
                return null;
            }
        }

        public string Path
        {
            get { return tbPath.Text; }
            set { tbPath.Text = value; }
        }

        public event EventHandler btnSaveClicked;
        public event EventHandler btnBrowseClicked;

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (Path.Length == 0)
            {
                new Views.Dialogs.ErrorDialog("Please, select a path");
                return;
            }

            if (ListTypeSelected == null)
            {
                new Views.Dialogs.ErrorDialog("Please, select a type of list");
                return;
            }

            if (ParserSelect == null)
            {
                new Views.Dialogs.ErrorDialog("Please, select a parser");
                return;
            }

            btnSaveClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            btnBrowseClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
