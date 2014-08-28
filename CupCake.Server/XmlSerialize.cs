using System.Xml;
using System.Xml.Serialization;
using JetBrains.Annotations;

namespace CupCake.Server
{
    public static class XmlSerialize
    {
        [NotNull]
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