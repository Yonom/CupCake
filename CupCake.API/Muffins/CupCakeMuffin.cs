using System;
using CupCake.Core.Platforms;
using CupCake.EE.Services;
using MuffinFramework.Muffins;

namespace CupCake.API.Muffins
{
    public abstract class CupCakeMuffin : CupCakeMuffin<object>
    {
    }

    public abstract class CupCakeMuffin<T> : Muffin<T>
    {
        private readonly Lazy<ConnectionPlatform> _connectionPlatform;
        private readonly Lazy<EEService> _eeService;
        private readonly Lazy<EventsPlatform> _eventsPlatform;

        protected CupCakeMuffin()
        {
            this._eventsPlatform = new Lazy<EventsPlatform>(() => this.PlatformLoader.Get<EventsPlatform>());
            this._connectionPlatform = new Lazy<ConnectionPlatform>(() => this.PlatformLoader.Get<ConnectionPlatform>());

            this._eeService = new Lazy<EEService>(() => this.ServiceLoader.Get<EEService>());
        }

        public EventsPlatform EventsPlatform
        {
            get { return this._eventsPlatform.Value; }
        }

        public ConnectionPlatform ConnectionPlatform
        {
            get { return this._connectionPlatform.Value; }
        }


        public EEService EEService
        {
            get { return this._eeService.Value; }
        }
    }
}