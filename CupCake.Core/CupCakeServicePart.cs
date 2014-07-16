using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Timers;
using CupCake.Core.Events;
using CupCake.Core.Log;
using CupCake.Core.Storage;
using MuffinFramework.Services;

namespace CupCake.Core
{
    public abstract class CupCakeServicePart<TProtocol> : ServicePart<TProtocol>
    {
        private readonly Lazy<ConnectionPlatform> _connectionPlatform;
        private readonly Lazy<EventManager> _events;
        private readonly Lazy<Logger> _logger;
        private readonly Lazy<string> _name;
        private readonly Lazy<StoragePlatform> _storagePlatform;
        private readonly Lazy<SynchronizePlatform> _synchronizePlatofrm;
        private readonly List<Timer> _timers = new List<Timer>();

        protected CupCakeServicePart()
        {
            _name = new Lazy<string>(FindName);

            _connectionPlatform = new Lazy<ConnectionPlatform>(() => PlatformLoader.Get<ConnectionPlatform>());
            _synchronizePlatofrm =
                new Lazy<SynchronizePlatform>(() => PlatformLoader.Get<SynchronizePlatform>());
            _storagePlatform = new Lazy<StoragePlatform>(() => PlatformLoader.Get<StoragePlatform>());

            _logger = new Lazy<Logger>(() =>
            {
                var logService = PlatformLoader.Get<LogPlatform>();
                string name = GetName();
                return new Logger(logService, name);
            });

            _events = new Lazy<EventManager>(() =>
            {
                var eventsPlatform = PlatformLoader.Get<EventsPlatform>();
                return new EventManager(eventsPlatform, this);
            });
        }

        protected Logger Logger
        {
            get { return _logger.Value; }
        }

        protected EventManager Events
        {
            get { return _events.Value; }
        }

        protected ConnectionPlatform ConnectionPlatform
        {
            get { return _connectionPlatform.Value; }
        }

        protected SynchronizePlatform SynchronizePlatform
        {
            get { return _synchronizePlatofrm.Value; }
        }

        protected StoragePlatform StoragePlatform
        {
            get { return _storagePlatform.Value; }
        }

        protected Timer GetTimer(int interval)
        {
            var timer = new Timer(interval) {SynchronizingObject = SynchronizePlatform.SynchronizingObject};
            _timers.Add(timer);
            return timer;
        }

        private string FindName()
        {
            var pluginName =
                (PluginNameAttribute)
                    Assembly.GetAssembly(GetType())
                        .GetCustomAttributes(typeof (PluginNameAttribute), false)
                        .FirstOrDefault();

            if (pluginName != null)
                return pluginName.Name;

            return GetType().Namespace;
        }

        protected virtual string GetName()
        {
            return _name.Value;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_events.IsValueCreated)
                {
                    _events.Value.Dispose();
                }

                foreach (Timer timer in _timers)
                {
                    timer.Dispose();
                }
            }

            base.Dispose(disposing);
        }
    }
}