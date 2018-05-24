using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinkle.Models;
using Twinkle.XML;
using System.IO;
using Twinkle.FileManager;

namespace Twinkle.Auth
{
    public class SavedToken
    {
        private const string _path = @"token/";
        private const string _name = @"/token.tk";

        public XMLManager<Token> XmlManager { get; set; }
        public string Path { get { return _path; } }
        public string Name { get { return _name; } }
        public bool IsSet { get { return File.Exists(_path + _name); } }

        public SavedToken()
        {
            XmlManager = new XMLManager<Token>();
        }

        public Token Load()
        {
            if (IsSet)
            {
                using (var fileReader = new FileReader(_path, _name))
                {
                    return XmlManager.Deserialize(fileReader.Stream);
                }
            }
            else
            {
                return null;
            }
        }

        public void Set(Token token)
        {
            using (var fileWriter = new FileWriter(_path, _name))
            {
                XmlManager.Serialize(fileWriter.Stream, token);
            }
        }
    }
}
