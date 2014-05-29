using System;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using CupCake.Chat.Services;
using CupCake.Command;
using CupCake.Command.Services;
using CupCake.Command.Source;
using CupCake.Core.Platforms;
using CupCake.Host;
using CupCake.Messages.Events.Receive;
using CupCake.Permissions;
using CupCake.Server.Output;
using CupCake.Server.SyntaxProviders;
using PlayerIOClient;

namespace CupCake.Server
{
    public class CupCakeClientEx
    {
        private const string GameId = "everybody-edits-su9rn58o40itdbnw69plyw";
        private CupCakeClient _client;

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
                new ExternalInvokeSource(this, Group.Host, "CupCakeClient", this.OnOutput),
                new ParsedCommand(input));
        }

        private void PlatformLoader_EnableComplete(object sender, EventArgs e)
        {
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

        public void Start(string email, string password, string roomId, string[] directories)
        {
            Client playerioclient = PlayerIO.QuickConnect.SimpleConnect(GameId, email, password);
            Connection connection = playerioclient.Multiplayer.JoinRoom(roomId, null);

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