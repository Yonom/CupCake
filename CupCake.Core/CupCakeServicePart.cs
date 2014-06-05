using System;
using CupCake.Core.Events;
using CupCake.Core.Log;
using MuffinFramework.Services;

namespace CupCake.Core
{
    public abstract class CupCakeServicePart<TProtocol> : ServicePart<TProtocol>
    {
        private readonly Lazy<ConnectionPlatform> _connectionPlatform;

        private readonly Lazy<EventManager> _events;
        private readonly Lazy<Logger> _logger;
        private readonly Lazy<SynchronizePlatform> _synchronizePlatofrm;

        protected CupCakeServicePart()
        {
            this._connectionPlatform = new Lazy<ConnectionPlatform>(() => this.PlatformLoader.Get<ConnectionPlatform>());
            this._synchronizePlatofrm =
                new Lazy<SynchronizePlatform>(() => this.PlatformLoader.Get<SynchronizePlatform>());

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

        protected Logger Logger
        {
            get { return this._logger.Value; }
        }

        protected EventManager Events
        {
            get { return this._events.Value; }
        }

        protected ConnectionPlatform ConnectionPlatform
        {
            get { return this._connectionPlatform.Value; }
        }

        protected SynchronizePlatform SynchronizePlatform
        {
            get { return this._synchronizePlatofrm.Value; }
        }

        protected virtual string GetName()
        {
            return this.GetType().Namespace;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Events.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}