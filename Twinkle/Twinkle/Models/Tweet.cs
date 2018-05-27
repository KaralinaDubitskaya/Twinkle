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
        public User User { get; set; }                // User who posted the tweet 
        public string Content { get; set; }           // Text of the tweet
        public List<string> Pictures { get; set; }    // Uploaded pictures
        public string Hashtags { get; set; }          
        public string Picture { get; set; }           // First uploaded picture (only one)

        public long ID { get; set; }                  // Tweet id

        // Tweet URL
        public string URL { get { return "https://twitter.com/" + User.ScreenName + "/status/" + ID.ToString(); } set { } }

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
            Hashtags = "";

            foreach (var item in tweet.Media)
            {
                if (item.MediaType == "photo")
                {
                    Pictures.Add(item.MediaURL);
                }
            }

            Picture = (Pictures.Count != 0) ? Pictures.First() : null;
            
            foreach (var item in tweet.Hashtags)
            {
                Hashtags += "#" + item.Text + "  ";
                // Remove hashtags from tweet text
                Content = Content.Remove(Content.IndexOf("#" + item.Text), item.Text.Length + 1);
            }
        }

        public override string ToString()
        {
            return String.Format("User: {0} \n Message: {1} \n\n", User.ScreenName, Content);
        }
    }
}
