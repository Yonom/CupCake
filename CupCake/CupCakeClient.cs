using System.Collections.Generic;
using System.Reflection;
using BotBits;
using PluginFramework;

namespace CupCake
{
    public class CupCakeClient : Package<CupCakeClient>
    {
        private readonly PluginLoader _pluginLoader = new PluginLoader();

        internal void Initialize(CupCakeArgs cupCakeArgs)
        {
            this.StartupArgs = cupCakeArgs.StartupArgs;

            this._pluginLoader.AddAssembly(Assembly.GetExecutingAssembly());
            foreach (var assembly in cupCakeArgs.Assemblies)
            {
                this._pluginLoader.AddAssembly(assembly);
            }

            var pluginContainer = new ContainerBase(this._pluginLoader);
            pluginContainer.LoadBotBits(this.BotBits);

            this._pluginLoader.LoadTypesWithBase<Service>(p => p.PreConstructor(this._pluginLoader, pluginContainer));
            pluginContainer.LoadServices();

            this._pluginLoader.LoadTypesWithBase<Plugin>(p => p.PreConstructor(this._pluginLoader, pluginContainer));
        }

        public IDictionary<string, object> StartupArgs { get; private set; }
    }
}