using System;
using CupCake.Core.Events;
using CupCake.Core.Log;
using CupCake.Core.Platforms;
using MuffinFramework.Services;

namespace CupCake.Core.Services
{
    public abstract class CupCakeServicePart<TProtocol> : ServicePart<TProtocol>
    {
        private readonly Lazy<ConnectionPlatform> _connectionPlatform;

        private readonly Lazy<EventManager> _events;
        private readonly Lazy<Logger> _logger;

        protected CupCakeServicePart()
        {
            this._connectionPlatform = new Lazy<ConnectionPlatform>(() => this.PlatformLoader.Get<ConnectionPlatform>());

            this._logger = new Lazy<Logger>(() =>
            {
                var logService = this.PlatformLoader.Get<LogPlatform>();
                string name = this.GetName();
                return new Logger(logService, name);
            });

            this._events = new Lazy<EventManager>(() =>
            {
                var eventsPlatform = this.PlatformLoader.Get<EventsPlatform>();
                return new EventManager(eventsPlatform, this);
            });
        }

        public Logger Logger
        {
            get { return this._logger.Value; }
        }

        public EventManager Events
        {
            get { return this._events.Value; }
        }

        public ConnectionPlatform ConnectionPlatform
        {
            get { return this._connectionPlatform.Value; }
        }

        public virtual string GetName()
        {
            return this.GetType().Namespace;
        }

        protected override void Dispose(bool disposing)
        {
            this.Events.Dispose();

            base.Dispose(disposing);
        }
    }
}