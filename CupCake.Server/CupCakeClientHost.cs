using System;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Threading;
using CupCake.Core;
using CupCake.Core.Events;
using CupCake.Core.Storage;
using CupCake.Host;
using CupCake.HostAPI;
using CupCake.HostAPI.IO;
using CupCake.HostAPI.Status;
using CupCake.HostAPI.Title;
using PlayerIOClient;
using Rabbit;

namespace CupCake.Server
{
    public class CupCakeClientHost
    {
        public const string GameId = "everybody-edits-su9rn58o40itdbnw69plyw";
        private CupCakeClient _client;
        private EventsPlatform _eventsPlatform;
        private IStorageProvider _storage;
        private SynchronizePlatform _synchronizePlatform;

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
            if (this._synchronizePlatform != null)
                this._synchronizePlatform.DoSynchronously(
                    () => { this._eventsPlatform.Event<InputEvent>().Raise(this, new InputEvent(input)); });
        }

        private void PlatformLoader_EnableComplete(object sender, EventArgs e)
        {
            if (this._storage != null)
            {
                // Change the default storage source
                var storagePlatform = this._client.PlatformLoader.Get<StoragePlatform>();
                storagePlatform.StorageProvider = this._storage;
            }

            this._synchronizePlatform = this._client.PlatformLoader.Get<SynchronizePlatform>();

            // Listen to HostAPI events
            this._eventsPlatform = this._client.PlatformLoader.Get<EventsPlatform>();
            this._eventsPlatform.Event<OutputEvent>().Bind(this.OnOutput, EventPriority.Lowest);
            this._eventsPlatform.Event<ChangeTitleEvent>().Bind(this.OnChangeTitle, EventPriority.Lowest);
            this._eventsPlatform.Event<ChangeStatusEvent>().Bind(this.OnChangeStatus, EventPriority.Lowest);
            this._eventsPlatform.Event<ShutdownRequestEvent>().Bind(this.OnShutdownRequest, EventPriority.Lowest);
        }

        private void OnShutdownRequest(object sender, ShutdownRequestEvent e)
        {
            if (e.IsRestarting)
            {
                Program.Restart();
            }

            this.Shutdown(e.IsRestarting 
                ? 2 
                : 0);
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
            this.Disconnected();
        }

        private void Disconnected()
        {
            this.LogMessage("Disconnected from Everybody Edits");
            Thread.Sleep(500); // Wait for all EE messages to arrive
            this.Shutdown(1);
        }

        private void Shutdown(int reason)
        {
            this.Dispose();
            Environment.Exit(reason);
        }

        private void LogMessage(string str)
        {
            this.OnOutput(String.Format("*** {0}", str));
        }

        public void Start(string email, string password, string worldId, string[] directories,
            IStorageProvider storage)
        {
            this._storage = storage;

            this.LogMessage(String.Format("Welcome to CupCake! (API version: {0})", this.GetVersion()));

            // Create the client
            this._client = new CupCakeClient(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            this._client.PlatformLoader.EnableComplete += this.PlatformLoader_EnableComplete;

            // Load plugins
            this.LogMessage("Loading plugin dlls...");
            foreach (string dir in directories)
            {
                if (Directory.Exists(dir))
                    this._client.AggregateCatalog.Catalogs.Add(new DirectoryCatalog(dir));
                else
                    this.LogMessage("Invalid folder: " + dir);
            }

            // Get room version
            this.LogMessage("Joining room...");
            // Connect to playerIO and join room
            Connection connection = new RabbitAuth().LogIn(email, worldId, password);

            // Start
            this.LogMessage("Starting plugins...");
            this._client.Start(new CupCakeClientArgs(connection,  worldId));

            // Handle disconnect, if we are already too late, disconnect
            connection.OnDisconnect += this.connection_OnDisconnect;
            if (!connection.Connected)
                this.Disconnected();

            this.LogMessage("Done.");
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

        public void Dispose()
        {
            if (this._client != null)
                this._client.Dispose();
        }
    }
}