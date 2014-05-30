using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace CupCake.Client.Settings
{
    public static class XmlSerialize
    {
        public static void Serialize<T>(T obj, string path)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var writer = new XmlTextWriter(path, Encoding.UTF8)
            {
                Formatting = Formatting.Indented,
                Indentation = 4
            })
            {
                serializer.Serialize(writer, obj);
            }
        }

        static public T Deserialize<T>(string path)
        {
            using (var reader = new XmlTextReader(path))
            {
                var deserializer = new XmlSerializer(typeof(T));
                object obj = deserializer.Deserialize(reader);
                T xmlData = (T)obj;
                return xmlData;
            }
        }
    }
}
