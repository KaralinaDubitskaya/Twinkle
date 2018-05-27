using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using Tweetinvi.Core.Interfaces;

namespace Twinkle.Models
{
    [Serializable]
    public class Tweet
    {
        public User User { get; set; }
        public string Content { get; set; }
        public List<string> Pictures { get; set; }
        public string Hashtegs { get; set; }
        public string Picture { get { return Pictures.First(); } set { } }
        public long ID { get; set; }

        public Tweet() { }

        public Tweet(User user, string content)
        {
            User = user;
            Content = content;
        }

        public Tweet(ITweet tweet)
        {
            Pictures = new List<string>();
            Content = tweet.Text;
            User = new User(tweet.CreatedBy);
            ID = tweet.Id;
            Hashtegs = "";

            foreach (var item in tweet.Media)
            {
                if (item.MediaType == "photo")
                {
                    Pictures.Add(item.MediaURL);
                }
            }

            var a = tweet.Text;


            foreach (var item in tweet.Hashtags)
            {
                Hashtegs += "#" + item.Text + "  ";
                Content = Content.Remove(Content.IndexOf("#" + item.Text), item.Text.Length + 1);
            }
        }

        public override string ToString()
        {
            return String.Format("User: {0} \n Message: {1} \n\n", User.ScreenName, Content);
        }
    }
}
