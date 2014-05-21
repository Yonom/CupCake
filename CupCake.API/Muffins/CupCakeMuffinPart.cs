using System;
using CupCake.Chat.Services;
using CupCake.Core.Events;
using CupCake.Core.Log;
using CupCake.Core.Platforms;
using CupCake.Players.Services;
using CupCake.World.Services;
using MuffinFramework.Muffins;

namespace CupCake.API.Muffins
{
    public abstract class CupCakeMuffinPart<TProtocol> : MuffinPart<TProtocol>
    {
        private readonly Lazy<Chatter> _chatter;
        private readonly Lazy<ConnectionPlatform> _connectionPlatform;
        private readonly Lazy<EventManager> _events;
        private readonly Lazy<Logger> _logger;
        private readonly Lazy<PlayerService> _playerService;
        private readonly Lazy<WorldService> _worldService;

        protected CupCakeMuffinPart()
        {
            this._connectionPlatform = new Lazy<ConnectionPlatform>(() => this.PlatformLoader.Get<ConnectionPlatform>());

            this._events = new Lazy<EventManager>(() =>
            {
                var eventsPlatform = this.PlatformLoader.Get<EventsPlatform>();
                return new EventManager(eventsPlatform, this);
            });
            this._logger = new Lazy<Logger>(() =>
            {
                var logService = this.PlatformLoader.Get<LogPlatform>();
                string name = this.GetName();
                return new Logger(logService, name);
            });

            this._chatter = new Lazy<Chatter>(() =>
            {
                var chatService = this.ServiceLoader.Get<ChatService>();
                string name = this.GetName();
                return new Chatter(chatService, name);
            });

            this._worldService = new Lazy<WorldService>(() => this.ServiceLoader.Get<WorldService>());
            this._playerService = new Lazy<PlayerService>(() => this.ServiceLoader.Get<PlayerService>());
        }

        public EventManager Events
        {
            get { return this._events.Value; }
        }

        public ConnectionPlatform ConnectionPlatform
        {
            get { return this._connectionPlatform.Value; }
        }

        public Logger Logger
        {
            get { return this._logger.Value; }
        }

        public Chatter Chatter
        {
            get { return this._chatter.Value; }
        }

        public WorldService WorldService
        {
            get { return this._worldService.Value; }
        }

        public PlayerService PlayerService
        {
            get { return this._playerService.Value; }
        }

        public virtual string GetName()
        {
            return this.GetType().Name;
        }

        protected override void Dispose(bool disposing)
        {
            this.Events.Dispose();

            base.Dispose(disposing);
        }
    }
}