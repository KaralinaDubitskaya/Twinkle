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

        // HomeTimeline, UserTimeLine, etc.
        public IList<string> TimeLines { set { cbTimeline.DataContext = value; } }
        public string SelectedTimeLine
        {
            get
            {
                if (cbFileTypes.SelectedItem != null)
                {
                    return cbTimeline.SelectedItem.ToString();
                }
                return null;
            }
        }

        // XMLParser, TextParser, etc.
        public IList<string> Parsers { set { cbFileTypes.DataContext = value; } }
        public string SelectedParser
        {
            get
            {
                if (cbFileTypes.SelectedItem != null)
                {
                    return cbFileTypes.SelectedItem.ToString();
                }
                return null;
            }
        }

        // Selected path
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

            if (SelectedTimeLine == null)
            {
                new Views.Dialogs.ErrorDialog("Please, select a type of list");
                return;
            }

            if (SelectedParser == null)
            {
                new Views.Dialogs.ErrorDialog("Please, select a parser");
                return;
            }

            btnSaveClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            // Choose file path
            btnBrowseClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
