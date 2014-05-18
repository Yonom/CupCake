using System;
using CupCake.Core.Platforms;
using MuffinFramework.Services;

namespace CupCake.Core.Services
{
    public abstract class CupCakeService : CupCakeService<object>
    {
    }

    public abstract class CupCakeService<T> : Service<T>
    {
        private readonly Lazy<ConnectionPlatform> _connectionPlatform;
        private readonly Lazy<EventsPlatform> _eventsPlatform;

        protected CupCakeService()
        {
            this._eventsPlatform = new Lazy<EventsPlatform>(() => this.PlatformLoader.Get<EventsPlatform>());
            this._connectionPlatform = new Lazy<ConnectionPlatform>(() => this.PlatformLoader.Get<ConnectionPlatform>());
        }

        public EventsPlatform EventsPlatform
        {
            get { return this._eventsPlatform.Value; }
        }

        public ConnectionPlatform ConnectionPlatform
        {
            get { return this._connectionPlatform.Value; }
        }
    }
}