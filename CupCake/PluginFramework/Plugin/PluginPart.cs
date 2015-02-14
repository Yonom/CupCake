using System;
using System.Collections.Generic;
using BotBits;
using CupCake.Command;
using CupCake.Log;
using CupCake.Storage;
using PluginFramework;

namespace CupCake
{
    public abstract class PluginPart : PluginPart<object>
    {

    }

    public abstract class PluginPart<THost> : ContainerPlugin
    {
        private string _name;
        private readonly List<ContainerPlugin> _loadedParts = new List<ContainerPlugin>(); 
        protected THost Host { get; private set; }

        protected PluginPart()
        {
            this.Commands = this.EnablePart<CommandManager, object>(null);
        }

        protected CommandManager Commands { get; private set; }

        public T EnablePart<T>() where T : PluginPart<THost>, new()
        {
            return this.EnablePart<T, THost>((THost)(object)this);
        }

        public T EnablePart<T>(THost host) where T : PluginPart<THost>, new()
        {
            return this.EnablePart<T, THost>(host);
        }

        public T EnablePart<T, TProtocol>(TProtocol host) where T : PluginPart<TProtocol>, new()
        {
            var t = this.Loader.Enable<T>(o =>
            {
                o.Host = host;
                o._name = this.GetName();
                o.PreConstructor(this.Loader, this.Container.BaseContainer);
            });

            lock (this._loadedParts)
                this._loadedParts.Add(t);

            return t;
        }

        public bool DisablePart(ContainerPlugin plugin)
        {
            lock (this._loadedParts)
            {
                if (!this._loadedParts.Remove(plugin)) 
                    return false;
            }
            
            ((IDisposable)plugin).Dispose();
            return true;
        }

        internal override string GetName()
        {
            return this._name;
        }

        protected override void Dispose(bool disposed)
        {
            ContainerPlugin[] loadedParts;
            lock (this._loadedParts)
            {
                loadedParts = this._loadedParts.ToArray();
                this._loadedParts.Clear();
            }

            foreach (var loadedPart in loadedParts)
            {
                ((IDisposable)loadedPart).Dispose();
            }

            base.Dispose(disposed);
        }
    }
}