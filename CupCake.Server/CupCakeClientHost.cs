using System;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using CupCake.Chat;
using CupCake.Core;
using CupCake.Core.Events;
using CupCake.Core.Storage;
using CupCake.Host;
using CupCake.HostAPI.IO;
using CupCake.HostAPI.Status;
using CupCake.HostAPI.Title;
using CupCake.Protocol;
using CupCake.Server.SyntaxProviders;
using PlayerIOClient;

namespace CupCake.Server
{
    public class CupCakeClientHost
    {
        public const string GameId = "everybody-edits-su9rn58o40itdbnw69plyw";
        private CupCakeClient _client;
        private EventsPlatform _eventsPlatform;
        private IStorageProvider _storage;

        public event Action<string> Output;

        protected virtual void OnOutput(string e)
        {
            Action<string> handler = this.Output;
            if (handler != null) handler(e);
        }

        public event Action<string> Title;

        protected virtual void OnTitle(string title)
        {
            Action<string> handler = this.Title;
            if (handler != null) handler(title);
        }

        public event Action<string> Status;

        protected virtual void OnStatus(string status)
        {
            Action<string> handler = this.Status;
            if (handler != null) handler(status);
        }

        public void Input(string input)
        {
            this._eventsPlatform.Event<InputEvent>().Raise(this, new InputEvent(input));
        }

        private void PlatformLoader_EnableComplete(object sender, EventArgs e)
        {
            if (this._storage != null)
            {
                // Change the default storage source
                var storagePlatform = this._client.PlatformLoader.Get<StoragePlatform>();
                storagePlatform.StorageProvider = this._storage;
            }

            // Listen to HostAPI events
            this._eventsPlatform = this._client.PlatformLoader.Get<EventsPlatform>();
            this._eventsPlatform.Event<OutputEvent>().Bind(this.OnOutput, EventPriority.Lowest);
            this._eventsPlatform.Event<ChangeTitleEvent>().Bind(this.OnChangeTitle, EventPriority.Lowest);
            this._eventsPlatform.Event<ChangeStatusEvent>().Bind(this.OnChangeStatus, EventPriority.Lowest);
        }

        private void ServiceLoader_EnableComplete(object sender, EventArgs e)
        {
            // Change the default chat, io and status formats
            this._client.ServiceLoader.Get<ChatService>().SyntaxProvider = new CupCakeChatSyntaxProvider();
            this._client.ServiceLoader.Get<IOService>().SyntaxProvider = new CupCakeIOSyntaxProvider();
            this._client.ServiceLoader.Get<StatusService>().SyntaxProvider = new CupCakeStatusSyntaxProvider();
        }

        private void OnOutput(object sender, OutputEvent e)
        {
            this.OnOutput(e.Message);
        }

        private void OnChangeTitle(object sender, ChangeTitleEvent e)
        {
            this.OnTitle(e.NewTitle);
        }

        private void OnChangeStatus(object sender, ChangeStatusEvent e)
        {
            this.OnStatus(e.NewStatus);
        }

        private void connection_OnDisconnect(object sender, string message)
        {
            this._client.Dispose();
            this.LogMessage("Disconnected from Everybody Edits");
            Environment.Exit(1);
        }

        private void LogMessage(string str)
        {
            this.OnOutput(String.Format("*** {0}", str));
        }

        public void Start(AccountType accType, string email, string password, string roomId, string[] directories,
            IStorageProvider storage)
        {
            this._storage = storage;

            this.LogMessage("Logging in...");
            // Connect to playerIO
            Client playerioclient = accType == AccountType.Regular
                ? PlayerIO.QuickConnect.SimpleConnect(GameId, email, password)
                : PlayerIO.QuickConnect.FacebookOAuthConnect(GameId, email, String.Empty);

            this.LogMessage("Login successful. Getting room version...");
            int version = RoomHelper.GetVersion();
            string roomType = RoomHelper.GetRoomType(roomId, version);


            this.LogMessage("Done. Joining room...");
            Connection connection = playerioclient.Multiplayer.CreateJoinRoom(roomId, roomType, true, null, null);
            connection.OnDisconnect += this.connection_OnDisconnect;

            this._client = new CupCakeClient(connection, new AssemblyCatalog(Assembly.GetExecutingAssembly()));

            this.LogMessage("Join complete. Loading plugin dlls...");
            foreach (string dir in directories)
            {
                this._client.AggregateCatalog.Catalogs.Add(new DirectoryCatalog(dir));
            }

            this._client.PlatformLoader.EnableComplete += this.PlatformLoader_EnableComplete;
            this._client.ServiceLoader.EnableComplete += this.ServiceLoader_EnableComplete;

            this.LogMessage("Getting stuff ready...");
            this._client.Start();
            this.LogMessage(String.Format("Welcome to CupCake! (API version: {0})", this.GetVersion()));
        }

        private string GetVersion()
        {
            var attribute =
                (AssemblyInformationalVersionAttribute)Assembly.GetAssembly(typeof(CupCakeClient))
                    .GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), false).FirstOrDefault();

            if (attribute != null)
                return attribute.InformationalVersion;
            return "Unknown!";
        }
    }
}