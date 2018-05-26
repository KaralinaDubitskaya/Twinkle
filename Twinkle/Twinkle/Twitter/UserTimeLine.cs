using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using Twinkle.Models;

namespace Twinkle.Twitter
{
    public class UserTimeLine : ListOfTweets
    {
        private const string _name = "UserTimeLine";
        public override string Name { get { return _name; } }

        public override Tweets GetTweets()
        {
            var userTimeLine = Timeline.GetUserTimeline(Tweetinvi.User.GetLoggedUser());
            Tweets tweets = new Tweets();

            if (userTimeLine != null)
            {
                Models.Tweet tweet;
                foreach (var item in userTimeLine)
                {
                    tweet = new Models.Tweet(item);
                    tweets += tweet;
                }
            }

            return tweets;
        }
    }
}
