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
    public static class SavedToken
    {
        // The folder in which the token is stored
        private const string _path = @"token/";
        // The file in which the token is stored
        private const string _name = @"/token.tk";

        public static string Path { get { return _path; } }
        public static string Name { get { return _name; } }

        // True if user was authorized before and set check box "Remember me"
        public static bool IsSet { get { return File.Exists(_path + _name); } }

        // Load token from the file and deserialize it
        public static Token Load()
        {
            // Token has been saved by user
            if (IsSet)
            {
                var XmlManager = new XMLManager<Token>();
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

        // Serialize token to XML and save it to the file
        public static void Set(Token token)
        {
            var XmlManager = new XMLManager<Token>();
            using (var fileWriter = new FileWriter(_path, _name))
            {
                XmlManager.Serialize(fileWriter.Stream, token);
            }
        }

        // Delete token when user want to LogOut
        public static void Delete()
        {
            if (IsSet)
            {
                File.Delete(_path + _name);
            }
        }
    }
}
