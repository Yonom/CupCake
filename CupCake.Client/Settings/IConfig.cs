using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CupCake.Client.Settings
{
    public interface IConfig
    {
        int Id { get; }
        string Name { get; }
        IConfig Clone();
    }
}
