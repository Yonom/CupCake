using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CupCake.Client.Settings
{
    public class Profile : IConfig
    {
        public string Name { get; set; }
        public string Folder { get; set; }
        public string Database { get; set; }

        public IConfig Clone()
        {
            return new Profile
            {
                Name = Name,
                Folder = Folder,
                Database = Database
            };
        }
    }
}
