using System;
using BotBits;
using CupCake.Command;
using CupCake.Log;
using CupCake.Storage;
using PluginFramework;

namespace CupCake
{
    public class ContainerPlugin : PluginBase
    {
        internal ContainerPlugin()
        {
        }
        
        internal Container Container { get; private set; }

        internal void PreConstructor(PluginLoader loader, ContainerBase containerBase)
        {
            this.Loader = loader;
            this.Container = containerBase.GetContainer(this.GetName());
        }

        protected PluginLoader Loader { get; private set; }

        internal virtual string GetName()
        {
            return "Bot";
        }

        internal Chat Chat
        {
            get { return this.Container.Chat; }
        }

        protected BotBitsClient BotBits
        {
            get { return this.Container.BotBits; }
        }
        
        protected Blocks Blocks
        {
            get { return this.Container.Blocks; }
        }

        protected Players Players
        {
            get { return this.Container.Players; }
        }

        protected Room Room
        {
            get { return this.Container.Room; }
        }

        protected Consumables Consumables
        {
            get { return this.Container.Consumables; }
        }

        protected Actions Actions
        {
            get { return this.Container.Actions; }
        }

        protected ConnectionManager ConnectionManager
        {
            get { return this.Container.ConnectionManager; }
        }

        protected EventManager Events
        {
            get { return this.Container.Events; }
        }

        protected Logger Logger
        {
            get { return this.Container.Logger; }
        }

        protected Chatter Chatter
        {
            get { return this.Container.Chatter; }
        }

        protected StorageService StorageService
        {
            get { return this.Container.StorageService; }
        }

        protected CommandService CommandService
        {
            get { return this.Container.CommandService; }
        }

        protected override void Dispose(bool disposed)
        {
            if (disposed)
            {
                this.Container.Dispose();
            }

            base.Dispose(disposed);
        }
    }
}
