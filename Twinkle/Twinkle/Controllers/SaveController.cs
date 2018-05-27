using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinkle.Models;
using Twinkle.Parser;
using Twinkle.Twitter;
using Twinkle.Views.Dialogs;

namespace Twinkle.Controllers
{
    public class SaveController : Controller
    {
        // SaveWindow
        public interface IWindow
        {
            event EventHandler btnSaveClicked;
            event EventHandler btnBrowseClicked;

            string Path { get; set; }

            IList<string> TimeLines { set; }
            string SelectedTimeLine { get; }

            IList<string> Parsers { set; }
            string SelectedParser { get; }

            void Show();
            void Hide();
        }

        private IWindow _window;

        public IWindow Window
        {
            get { return _window; }
            set
            {
                _window = value;
                _window.btnSaveClicked += Window_btnSaveClicked;
                _window.btnBrowseClicked += Window_btnBrowseClicked;
            }
        }

        // Avaible parsers (Text, XML, etc.)
        public IList<Parser.Parser> Parsers { get; set; }
        // Avaible timelines (HomeTimeLine, UserTimeLine)
        public IList<ListOfTweets> TimeLines { get; set; }

        private void Window_btnSaveClicked(object sender, EventArgs e)
        {
            // Load selected timeline
            Tweets tweets = (from i in TimeLines where i.Name == Window.SelectedTimeLine select i).First().GetTweets();
            // Load se;ected parser
            Parser.Parser parser = (from i in Parsers where i.Name == Window.SelectedParser select i).First();

            if (parser == null)
            {
                new ErrorDialog("There is no parser with this name");
                return;
            }

            if (tweets == null)
            {
                new ErrorDialog("There was a problem loading the list");
                return;
            }

            try
            {
                string path;
                if (Window.Path.EndsWith(parser.Extension))
                {
                    path = Window.Path;
                }
                else
                {
                    // Add proper extension
                    path = String.Concat(Window.Path, parser.Extension);
                }

                FileManager.FileWriter fileManager = new FileManager.FileWriter(path);
                parser.Save(tweets, fileManager.Stream);
                fileManager.Dispose();

                SuccessDialog successDialog = new SuccessDialog("The backup was successfully performed");
                Window.Hide();

            }
            catch (Exception ex)
            {
                new ErrorDialog(ex.Message);
            }

        }

        private void Window_btnBrowseClicked(object sender, EventArgs e)
        {
            // User selects the path to the file
            PathDialog pathDialog = new PathDialog();
            pathDialog.OnPathChanged += PathDialog_OnPathChanged;
            pathDialog.OpenPathDialog();
        }

        private void PathDialog_OnPathChanged(object sender, PathDialogEventArgs e)
        {
            Window.Path = e.Path;
        }

        public override void HandleNavigation(object args)
        {
            // Load types of timelines
            TimeLines = new List<ListOfTweets> { new HomeTimeLine(), new UserTimeLine() };
            Window.TimeLines = (from i in TimeLines select i.Name).ToList();

            // Load types of parsers
            Parsers = new List<Parser.Parser> { new XmlParser(), new TextParser() };
            Window.Parsers = (from i in Parsers select i.Name).ToList();

            // Show SaveWindow
            Window.Show();
        }


    }
}
