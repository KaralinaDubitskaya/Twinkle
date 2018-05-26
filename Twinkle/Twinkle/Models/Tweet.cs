using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi.Core.Interfaces;

namespace Twinkle.Models
{
    [Serializable]
    public class Tweet
    {
        public User User { get; set; }
        public string Content { get; set; }
        public List<string> Pictures { get; set; }
        public string Picture { get { return Pictures.First(); } set { } }

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

            foreach (var item in tweet.Media)
            {
                if (item.MediaType == "photo")
                {
                    Pictures.Add(item.MediaURL);
                }
            }
        }

        public override string ToString()
        {
            return String.Format("User: {0} \n Message: {1} \n\n", User.ScreenName, Content);
        }
    }
}
