using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CupCake.Client.Settings
{
    public class Profile : IConfig
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Folder { get; set; }
        public int Database { get; set; }

        public static Profile NewEmpty()
        {
            return new Profile {Id = ++SettingsManager.Settings.LastProfileId};
        }
    }
}
