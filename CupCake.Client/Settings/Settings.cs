using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CupCake.Client.Settings
{
    public class Settings
    {
        public Settings()
        {
            Accounts = new List<Account>();
            Profiles = new List<Profile>();
            RecentWorlds = new List<RecentWorld>();
        }

        public List<Account> Accounts { get; set; }
        public List<Profile> Profiles { get; set; }
        public List<RecentWorld> RecentWorlds { get; set; }

        public int LastProfileId { get; set; }
        public int LastAccountId { get; set; }
        public int LastRecentWorldId { get; set; }

        public string LastAttachAddress { get; set; }
        public string LastAttachPin { get; set; }
    }
}
