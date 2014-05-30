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
        }

        public List<Account> Accounts { get; set; }
        public List<Profile> Profiles { get; set; }
    }
}
