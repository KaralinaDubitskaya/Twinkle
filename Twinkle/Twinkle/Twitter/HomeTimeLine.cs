using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinkle.Models;
using Tweetinvi;

namespace Twinkle.Twitter
{
    public class HomeTimeLine : ListOfTweets
    {
        private const string _name = "HomeTimeLine";
        public override string Name { get { return _name; } }

        public override Tweets GetTweets()
        {
            var homeTimeLine = Timeline.GetHomeTimeline();
            Tweets tweets = new Tweets();

            if (homeTimeLine != null)
            {
                Models.Tweet tweet;
                foreach (var item in homeTimeLine)
                {
                    tweet = new Models.Tweet(item);
                    tweets += tweet;
                }
            }

            return tweets;
        }
    }
}
