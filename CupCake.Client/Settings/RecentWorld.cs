using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CupCake.Client.Settings
{
    public class RecentWorld : IConfig
    {
        public string WorldId { get; set; }
        public int Profile { get; set; }
        public int Account { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }

        public IConfig Clone()
        {
            throw new NotImplementedException();
        }
    }
}
