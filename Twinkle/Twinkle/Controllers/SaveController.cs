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

        public interface IWindow
        {
            event EventHandler btnSaveClicked;
            event EventHandler btnBrowseClicked;

            string Path { get; set; }

            IList<string> ListTypes { set; }
            string ListTypeSelected { get; }

            IList<string> ParserNames { set; }
            string ParserSelect { get; }

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
        public IList<Parser.Parser> Parsers { get; set; }
        public IList<ListOfTweets> Tweets { get; set; }

        private void Window_btnSaveClicked(object sender, EventArgs e)
        {
            Tweets tweets = (from i in Tweets where i.Name == Window.ListTypeSelected select i).First().GetTweets();
            Parser.Parser parser = (from i in Parsers where i.Name == Window.ParserSelect select i).First();

            if (parser == null)
            {
                new Twinkle.Views.Dialogs.ErrorDialog("There is no parser with this name");
                return;
            }

            if (tweets == null)
            {
                new Twinkle.Views.Dialogs.ErrorDialog("There was a problem loading the list");
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
                    path = String.Concat(Window.Path, parser.Extension);
                }

                FileManager.FileWriter fileManager = new FileManager.FileWriter(path);
                parser.Save(tweets, fileManager.Stream);
                fileManager.Dispose();

                Views.Dialogs.SuccessDialog successDialog = new SuccessDialog("The backup was successfully performed");
                Window.Hide();

            }
            catch (Exception ex)
            {
                new Views.Dialogs.ErrorDialog(ex.Message);
            }

        }

        private void Window_btnBrowseClicked(object sender, EventArgs e)
        {
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
            Tweets = new List<ListOfTweets> { new HomeTimeLine(), new UserTimeLine() };
            Window.ListTypes = (from i in Tweets select i.Name).ToList();

            Parsers = new List<Parser.Parser> { new XmlParser(), new TextParser() };
            Window.ParserNames = (from i in Parsers select i.Name).ToList();

            Window.Show();
        }


    }
}
