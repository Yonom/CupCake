using System.Collections.Generic;
using System.Xml.Serialization;
using CupCake.Protocol;

namespace CupCake.Server
{
    public class CupCakeServerSettings
    {
        public string ConnectionString;
        public DatabaseType DatabaseType;
        public bool Debug;
        public string Email;
        public string Password;
        public string Pin;
        public int Port;
        public bool Standalone;
        public string World;

        private List<string> _dirs;

        [XmlElement("Directory")]
        public List<string> Dirs
        {
            get { return this._dirs ?? (this._dirs = new List<string>()); }
            set { this._dirs = value; }
        }
    }
}