using BotBits;
using CupCake.Command;
using CupCake.Log;
using CupCake.Permissions;
using CupCake.Storage;
using PluginFramework;

namespace CupCake
{
    internal class ContainerBase
    {
        private readonly PluginLoader _loader;

        public ContainerBase(PluginLoader loader)
        {
            this._loader = loader;
        }

        public void LoadBotBits(BotBitsClient client)
        {
            this.BotBits = client;
            this.Blocks = Blocks.Of(client);
            this.Players = Players.Of(client);
            this.Room = Room.Of(client);
            this.Chat = Chat.Of(client);
            this.Consumables = Consumables.Of(client);
            this.Actions = Actions.Of(client);
            this.ConnectionManager = ConnectionManager.Of(client);
            this.BlockChecker = BlockChecker.Of(client);
        }

        public void LoadServices()
        {
            this.LogService = this._loader.Get<LogService>();
            this.StorageService = this._loader.Get<StorageService>();
            this.ChatService = this._loader.Get<ChatService>();
            this.PermissionService = this._loader.Get<PermissionService>();
            this.CommandService = this._loader.Get<CommandService>();
        }

        public Container GetContainer(string name)
        {
            return new Container(this, name);
        }

        // BotBits
        public BotBitsClient BotBits { get; private set; }
        public Blocks Blocks { get; private set; }
        public Players Players { get; private set; }
        public Room Room { get; private set; }
        public Chat Chat { get; private set; }
        public Consumables Consumables { get; private set; }
        public Actions Actions { get; private set; }
        public ConnectionManager ConnectionManager { get; private set; }
        public BlockChecker BlockChecker { get; private set; }
        
        // Services
        public LogService LogService { get; private set; }
        public ChatService ChatService { get; private set; }
        public StorageService StorageService { get; private set; }
        public PermissionService PermissionService { get; private set; }
        public CommandService CommandService { get; private set; }
    }
}