using System;
using CupCake.Chat.Services;
using CupCake.Core.Platforms;
using CupCake.Log.Services;
using CupCake.World;
using MuffinFramework.Muffins;

namespace CupCake.API.Muffins
{
    public abstract class CupCakeMuffin : CupCakeMuffin<object>
    {
    }

    public abstract class CupCakeMuffin<T> : Muffin<T>
    {
        private readonly Lazy<ConnectionPlatform> _connectionPlatform;
        private readonly Lazy<EventsPlatform> _eventsPlatform;
        private readonly Lazy<Logger> _logger;
        private readonly Lazy<Chatter> _chatter;
        private readonly Lazy<WorldService> _worldService;

        protected CupCakeMuffin()
        {
            this._eventsPlatform = new Lazy<EventsPlatform>(() => this.PlatformLoader.Get<EventsPlatform>());
            this._connectionPlatform = new Lazy<ConnectionPlatform>(() => this.PlatformLoader.Get<ConnectionPlatform>());

            this._logger = new Lazy<Logger>(() =>
            {
                var logService = this.ServiceLoader.Get<LogService>();
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
        }

        public EventsPlatform EventsPlatform
        {
            get { return this._eventsPlatform.Value; }
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

        public virtual string GetName()
        {
            return this.GetType().Name;
        }
    }
}