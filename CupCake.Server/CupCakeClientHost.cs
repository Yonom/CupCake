using System;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using CupCake.Chat;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Core;
using CupCake.Host;
using CupCake.Messages.Receive;
using CupCake.Permissions;
using CupCake.Protocol;
using CupCake.Server.Output;
using CupCake.Server.StorageProviders;
using CupCake.Server.SyntaxProviders;
using PlayerIOClient;

namespace CupCake.Server
{
    public class CupCakeClientHost
    {
        public const string GameId = "everybody-edits-su9rn58o40itdbnw69plyw";
        private CupCakeClient _client;
        private string _cs;
        private DatabaseType _dbType;

        public event Action<string> Output;

        protected virtual void OnOutput(string e)
        {
            Action<string> handler = this.Output;
            if (handler != null) handler(e);
        }

        public event Action<string> Title;

        protected virtual void OnTitle(string obj)
        {
            Action<string> handler = this.Title;
            if (handler != null) handler(obj);
        }

        public void Input(string input)
        {
            this.OnOutput("> " + input);

            this._client.ServiceLoader.Get<CommandService>().Invoke(
                new ExternalInvokeSource(this, Group.Host, "CupCakeHost", this.OnOutput),
                new ParsedCommand(input));
        }

        private void PlatformLoader_EnableComplete(object sender, EventArgs e)
        {
            var storagePlatform = this._client.PlatformLoader.Get<StoragePlatform>();
            if (this._dbType == DatabaseType.MySql)
                storagePlatform.StorageProvider = new MySqlStorageProvider(this._cs);
            else
                storagePlatform.StorageProvider = new SQLiteStorageProvider(this._cs);

            var eventsPlatform = this._client.PlatformLoader.Get<EventsPlatform>();
            eventsPlatform.Event<CupCakeOutputEvent>().Bind(this.OnCupCakeOutput);
            eventsPlatform.Event<UpdateMetaReceiveEvent>().Bind(this.OnUpdateMeta);
        }

        private void ServiceLoader_EnableComplete(object sender, EventArgs e)
        {
            this._client.ServiceLoader.Get<ChatService>().ChatSyntaxProvider = new CupCakeChatSyntaxProvider();
        }

        private void OnCupCakeOutput(object sender, CupCakeOutputEvent e)
        {
            this.OnOutput(e.Message);
        }

        private void OnUpdateMeta(object sender, UpdateMetaReceiveEvent e)
        {
            this.OnTitle(e.WorldName);
        }

        private void connection_OnDisconnect(object sender, string message)
        {
            this._client.Dispose();
            this.OnOutput("*** Disconnected from Everybody Edits ***");
            Environment.Exit(1);
        }

        public void Start(AccountType accType, string email, string password, string roomId, string[] directories,
            DatabaseType dbType, string cs)
        {
            this._dbType = dbType;
            this._cs = cs;

            // Connect to playerIO
            Client playerioclient = accType == AccountType.Regular
                ? PlayerIO.QuickConnect.SimpleConnect(GameId, email, password)
                : PlayerIO.QuickConnect.FacebookOAuthConnect(GameId, email, String.Empty);

            int version = RoomHelper.GetVersion();
            string roomType = RoomHelper.GetRoomType(roomId, version);
            Connection connection = playerioclient.Multiplayer.CreateJoinRoom(roomId, roomType, true, null, null);
            connection.OnDisconnect += this.connection_OnDisconnect;

            this._client = new CupCakeClient(connection, new AssemblyCatalog(Assembly.GetExecutingAssembly()));

            foreach (string dir in directories)
            {
                this._client.AggregateCatalog.Catalogs.Add(new DirectoryCatalog(dir));
            }

            this._client.PlatformLoader.EnableComplete += this.PlatformLoader_EnableComplete;
            this._client.ServiceLoader.EnableComplete += this.ServiceLoader_EnableComplete;
            this._client.Start();
        }
    }
}