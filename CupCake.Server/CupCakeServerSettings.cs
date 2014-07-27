using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using CupCake.Protocol;

namespace CupCake.Server
{
    public class CupCakeServerSettings
    {
        public int Port;
        public string Pin;
        public string Email;
        public string Password;
        public string World;
        public DatabaseType DatabaseType;
        public string ConnectionString;
        public bool Debug;
        public bool Standalone;

        private List<string> _dirs;

        [XmlElement("Directory")]
        public List<string> Dirs {
            get { return this._dirs ?? (this._dirs = new List<string>()); }
            set { _dirs = value; }
        }
    }
}
