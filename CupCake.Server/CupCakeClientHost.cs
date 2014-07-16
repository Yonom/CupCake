using System;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using CupCake.Chat;
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
            Action<string> handler = Output;
            if (handler != null) handler(e);
        }

        public event Action<string> Title;

        protected virtual void OnTitle(string title)
        {
            Action<string> handler = Title;
            if (handler != null) handler(title);
        }

        public event Action<string> Status;

        protected virtual void OnStatus(string status)
        {
            Action<string> handler = Status;
            if (handler != null) handler(status);
        }

        public void Input(string input)
        {
            _eventsPlatform.Event<InputEvent>().Raise(this, new InputEvent(input));
        }

        private void PlatformLoader_EnableComplete(object sender, EventArgs e)
        {
            if (_storage != null)
            {
                // Change the default storage source
                var storagePlatform = _client.PlatformLoader.Get<StoragePlatform>();
                storagePlatform.StorageProvider = _storage;
            }

            // Listen to HostAPI events
            _eventsPlatform = _client.PlatformLoader.Get<EventsPlatform>();
            _eventsPlatform.Event<OutputEvent>().Bind(OnOutput, EventPriority.Lowest);
            _eventsPlatform.Event<ChangeTitleEvent>().Bind(OnChangeTitle, EventPriority.Lowest);
            _eventsPlatform.Event<ChangeStatusEvent>().Bind(OnChangeStatus, EventPriority.Lowest);
        }

        private void ServiceLoader_EnableComplete(object sender, EventArgs e)
        {
            // Change the default chat, io and status formats
            _client.ServiceLoader.Get<ChatService>().SyntaxProvider = new CupCakeChatSyntaxProvider();
            _client.ServiceLoader.Get<IOService>().SyntaxProvider = new CupCakeIOSyntaxProvider();
            _client.ServiceLoader.Get<StatusService>().SyntaxProvider = new CupCakeStatusSyntaxProvider();
        }

        private void OnOutput(object sender, OutputEvent e)
        {
            OnOutput(e.Message);
        }

        private void OnChangeTitle(object sender, ChangeTitleEvent e)
        {
            OnTitle(e.NewTitle);
        }

        private void OnChangeStatus(object sender, ChangeStatusEvent e)
        {
            OnStatus(e.NewStatus);
        }

        private void connection_OnDisconnect(object sender, string message)
        {
            Disconnected();
        }

        private void Disconnected()
        {
            LogMessage("Disconnected from Everybody Edits");
            Dispose();
            Environment.Exit(1);
        }

        private void LogMessage(string str)
        {
            OnOutput(String.Format("*** {0}", str));
        }

        public void Start(AccountType accType, string email, string password, string roomId, string[] directories,
            IStorageProvider storage)
        {
            _storage = storage;

            LogMessage(String.Format("Welcome to CupCake! (API version: {0})", GetVersion()));

            // Create the client
            _client = new CupCakeClient(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            _client.PlatformLoader.EnableComplete += PlatformLoader_EnableComplete;
            _client.ServiceLoader.EnableComplete += ServiceLoader_EnableComplete;

            // Load plugins
            LogMessage("Loading plugin dlls...");
            foreach (string dir in directories)
            {
                if (Directory.Exists(dir))
                    _client.AggregateCatalog.Catalogs.Add(new DirectoryCatalog(dir));
                else
                    LogMessage("Invalid folder: " + dir);
            }

            // Get room version
            LogMessage("Getting room version...");
            int version = RoomHelper.GetVersion();
            string roomType = RoomHelper.GetRoomType(roomId, version);

            // Connect to playerIO
            LogMessage("Logging in...");
            Client playerioclient = accType == AccountType.Regular
                ? PlayerIO.QuickConnect.SimpleConnect(GameId, email, password)
                : PlayerIO.QuickConnect.FacebookOAuthConnect(GameId, email, String.Empty);

            // Join room
            LogMessage("Joining room...");
            Connection connection = playerioclient.Multiplayer.CreateJoinRoom(roomId, roomType, true, null, null);

            // Start
            LogMessage("Starting plugins...");
            _client.Start(connection);

            // Handle disconnect, if we are already too late, disconnect
            connection.OnDisconnect += connection_OnDisconnect;
            if (!connection.Connected)
                Disconnected();

            LogMessage("Done.");
        }

        private string GetVersion()
        {
            var attribute =
                (AssemblyInformationalVersionAttribute) Assembly.GetAssembly(typeof (CupCakeClient))
                    .GetCustomAttributes(typeof (AssemblyInformationalVersionAttribute), false).FirstOrDefault();

            if (attribute != null)
                return attribute.InformationalVersion;
            return "Unknown!";
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}