using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinkle.Models;

namespace Twinkle.Twitter
{
    public abstract class ListOfTweets
    {
        public abstract string Name { get; }
        public abstract Tweets GetTweets();
    }
}
