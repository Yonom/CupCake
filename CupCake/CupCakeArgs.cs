using System.Collections.Generic;
using System.Reflection;

namespace CupCake
{
    public class CupCakeArgs
    {
        public Assembly[] Assemblies { get; set; }
        public IDictionary<string, object> StartupArgs { get; private set; }

        public CupCakeArgs(Assembly[] assemblies)
            : this (assemblies, new Dictionary<string, object>())
        {
            
        }

        public CupCakeArgs(Assembly[] assemblies, string worldId)
            : this(assemblies, new Dictionary<string, object> { { "WorldId", worldId } })
        {
        }

        public CupCakeArgs(Assembly[] assemblies, IDictionary<string, object> startupArgs)
        {
            this.Assemblies = assemblies;
            this.StartupArgs = startupArgs;
        }
    }
}