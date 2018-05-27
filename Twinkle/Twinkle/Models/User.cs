using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi.Core.Interfaces;

namespace Twinkle.Models
{
    [Serializable]
    public class User
    {
        public string Username { get; set; }
        public string ScreenName { get; set; }
        public string ProfileImageUrl { get; set; }
        public string Description { get; set; }

        public int Tweets { get; set; }
        public int Followings { get; set; }   // number of friends
        public int Followers { get; set; }    // number of followers
        public long ID { get; set; }          // user id

        public string City { get; set; }
        public string AccountCreatedAt { get; set; }

        public string Admin { get; set; }     // is logged user
        public string Follow { get; set; }    // is followed by logged user

        // Max length of the description
        private const int _maxDescriptionLen = 35;   

        public User() { }

        public User(IUser user)
        {
            Username = user.Name;
            ScreenName = user.ScreenName;
            ProfileImageUrl = user.ProfileImageUrl400x400;
            Tweets = user.StatusesCount;
            Followers = user.FollowersCount;
            Followings = user.FriendsCount;
            ID = user.Id;
            Description = user.Description;
            if (Description.Length > _maxDescriptionLen)
            {
                // Cut the end of the description
                Description = Description.Substring(0, _maxDescriptionLen - 3) + "...";
            }

            City = user.Location;
            AccountCreatedAt = "Joined " + String.Format("{0:MMMM  yyyy}", user.CreatedAt);

            Admin = "Visible";    // not logged user
            Follow = "Follow";    // isn't followed by the logged user
        }
    }
}
