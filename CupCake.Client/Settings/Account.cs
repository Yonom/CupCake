using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using CupCake.Protocol;

namespace CupCake.Client.Settings
{
    public class Account : IConfig
    {
        public AccountType Type { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Id { get; set; }

        string IConfig.Name {
            get { return this.Email; }
        }

        public static Account NewEmpty()
        {
            return new Account { Id = ++SettingsManager.Settings.LastAccountId };
        }
    }
}
