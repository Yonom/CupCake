using System;
using BotBits;
using CupCake.Command;
using CupCake.Log;
using CupCake.Permissions;
using CupCake.Storage;
using PluginFramework;

namespace CupCake
{
    internal class Container
    {
        public ContainerBase BaseContainer { get; private set; }

        public Container(ContainerBase baseContainer, string name)
        {
            this.BaseContainer = baseContainer;
            this.Events = new EventManager(this.BotBits);
            this.Logger = new Logger(this.BaseContainer.LogService, name);
            this.Chatter = new Chatter(this.BaseContainer.ChatService, name);
        }

        public EventManager Events { get; private set; }
        public Chatter Chatter { get; private set; }
        public Logger Logger { get; private set; }

        public BotBitsClient BotBits
        {
            get { return this.BaseContainer.BotBits; }
        }

        public Blocks Blocks
        {
            get { return this.BaseContainer.Blocks; }
        }

        public BlockChecker BlockChecker
        {
            get { return this.BaseContainer.BlockChecker; }
        }

        public Players Players
        {
            get { return this.BaseContainer.Players; }
        }

        public Room Room
        {
            get { return this.BaseContainer.Room; }
        }

        public Chat Chat
        {
            get { return this.BaseContainer.Chat; }
        }

        public Consumables Consumables
        {
            get { return this.BaseContainer.Consumables; }
        }

        public Actions Actions
        {
            get { return this.BaseContainer.Actions; }
        }

        public ConnectionManager ConnectionManager
        {
            get { return this.BaseContainer.ConnectionManager; }
        }

        public StorageService StorageService
        {
            get { return this.BaseContainer.StorageService; }
        }

        public PermissionService PermissionService
        {
            get { return this.BaseContainer.PermissionService; }
        }

        public CommandService CommandService
        {
            get { return this.BaseContainer.CommandService; }
        }

        public void Dispose()
        {
            ((IDisposable)this.Events).Dispose();
        }
    }
}