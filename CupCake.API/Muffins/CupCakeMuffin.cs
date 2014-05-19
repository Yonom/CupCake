using System;
using CupCake.Core.Platforms;
using CupCake.Log.Services;
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

        protected CupCakeMuffin()
        {
            this._eventsPlatform = new Lazy<EventsPlatform>(() => this.PlatformLoader.Get<EventsPlatform>());
            this._connectionPlatform = new Lazy<ConnectionPlatform>(() => this.PlatformLoader.Get<ConnectionPlatform>());

            this._logger = new Lazy<Logger>(() =>
            {
                var logService = this.ServiceLoader.Get<LogService>();
                var name = this.GetName();
                return new Logger(logService, name);
            });
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

        public virtual string GetName()
        {
            return this.GetType().Name;
        }
    }
}