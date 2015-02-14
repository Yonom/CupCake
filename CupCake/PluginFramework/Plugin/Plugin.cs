using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using PluginFramework;

namespace CupCake
{
    [AutoLoad]
    public abstract class Plugin<TSelf> : PluginPart<TSelf>
    {
        internal override string GetName()
        {
            return this.GetType().Namespace;
        }
    }

    public abstract class Plugin : Plugin<object>
    {
        
    }
}
