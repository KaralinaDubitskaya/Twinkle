using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace Twinkle.XML
{
    public class XMLManager<T>
    {
        public void Serialize(StreamWriter streamWriter, T content)
        {
            if (content != null)
            {
                XmlSerializer serializer = new XmlSerializer(content.GetType());
                serializer.Serialize(streamWriter, content);
            }
        }

        public T Deserialize(Stream stream)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(stream);
        }
    }
}
