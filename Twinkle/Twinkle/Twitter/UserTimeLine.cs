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

        private long _id;

        public UserTimeLine()
        {
            _id = Tweetinvi.User.GetLoggedUser().Id;
        }

        public UserTimeLine(long id)
        {
            _id = id;
        }

        public override Tweets GetTweets()
        {
            var userTimeLine = Timeline.GetUserTimeline(_id);
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
