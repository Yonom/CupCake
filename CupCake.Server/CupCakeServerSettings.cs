using System.Collections.Generic;
using System.Xml.Serialization;
using CupCake.Protocol;

namespace CupCake.Server
{
    public class CupCakeServerSettings
    {
        public string ConnectionString;
        public DatabaseType DatabaseType { get; set; }
        public bool Debug { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Pin { get; set; }
        public int Port { get; set; }
        public bool Standalone { get; set; }
        public string World { get; set; }
        public bool Autoconnect { get; set; }

        private List<string> _dirs;

        [XmlElement("Directory")]
        public List<string> Dirs
        {
            get { return this._dirs ?? (this._dirs = new List<string>()); }
            set { this._dirs = value; }
        }
    }
}