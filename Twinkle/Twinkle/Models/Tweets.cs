using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twinkle.Models
{
    public class Tweets : List<Tweet>
    {
        public static Tweets operator +(Tweets list, Tweet tweet)
        {
            list.Add(tweet);
            return list;
        }
    }
}
