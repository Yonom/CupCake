using System.Xml;
using System.Xml.Serialization;

namespace CupCake.Server
{
    public static class XmlSerialize
    {
        public static T Deserialize<T>(string path)
        {
            using (var reader = new XmlTextReader(path))
            {
                var deserializer = new XmlSerializer(typeof(T));
                object obj = deserializer.Deserialize(reader);
                var xmlData = (T)obj;
                return xmlData;
            }
        }
    }
}