using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twinkle.Models
{
    public class Token
    {
        // Username
        public string Name { get; set; }

        // A pair of credentials, that will be specific to a user and an application
        public string AccessToken { get; set; }
        public string AccessTokenSecret { get; set; }

        public Token() { }

        public Token(string name, string accessToken, string accessTokenSecret)
        {
            Name = name;
            AccessToken = accessToken;
            AccessTokenSecret = accessTokenSecret;
        }
    }
}
