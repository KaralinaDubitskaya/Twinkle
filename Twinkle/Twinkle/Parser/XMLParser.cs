using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinkle.Models;
using System.IO;
using System.Xml.Serialization;

namespace Twinkle.Parser
{
    public class XmlParser : Parser
    {
        private const string _extension = ".xml";
        private const string _name = "Xml";

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
            XmlSerializer xmlSerializer = new XmlSerializer(tweets.GetType());
            xmlSerializer.Serialize(stream, tweets);
        }

    }
}
