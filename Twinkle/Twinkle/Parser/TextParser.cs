using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinkle.Models;
using System.IO;

namespace Twinkle.Parser
{
    public class TextParser : Parser
    {

        private const string _extension = ".txt";
        private const string _name = "Text";

        public override string Extension
        {
            get { return _extension; }
        }

        public override string Name
        {
            get { return _name; }
        }


        public override void Save(Tweets tweets, StreamWriter stream)
        {
            using (stream)
            {
                foreach (Tweet item in tweets)
                {
                    stream.WriteLine(item.ToString());
                }
            }
        }
    }
}
