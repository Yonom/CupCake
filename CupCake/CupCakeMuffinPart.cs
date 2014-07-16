using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Timers;
using CupCake.Actions;
using CupCake.Chat;
using CupCake.Command;
using CupCake.Core;
using CupCake.Core.Events;
using CupCake.Core.Log;
using CupCake.Core.Storage;
using CupCake.HostAPI.IO;
using CupCake.HostAPI.Status;
using CupCake.Keys;
using CupCake.Messages;
using CupCake.Permissions;
using CupCake.Players;
using CupCake.Potions;
using CupCake.Room;
using CupCake.Upload;
using CupCake.World;
using MuffinFramework.Muffins;

namespace CupCake
{
    /// <summary>
    ///     Class CupCakeMuffinPart.
    /// </summary>
    /// <typeparam name="TProtocol">The type of the protocol.</typeparam>
    public abstract class CupCakeMuffinPart<TProtocol> : MuffinPart<TProtocol>
    {
        private readonly Lazy<ActionService> _actionService;
        private readonly Lazy<Chatter> _chatter;
        private readonly Lazy<CommandService> _commandService;
        private readonly Lazy<ConnectionPlatform> _connectionPlatform;
        private readonly Lazy<EventManager> _events;
        private readonly Lazy<IOService> _ioService;
        private readonly Lazy<KeyService> _keyService;
        private readonly Lazy<Logger> _logger;
        private readonly Lazy<MessageService> _messageService;
        private readonly Lazy<string> _name;
        private readonly Lazy<PermissionService> _permissionService;
        private readonly Lazy<PlayerService> _playerService;
        private readonly Lazy<PotionService> _potionService;
        private readonly Lazy<RoomService> _roomService;
        private readonly Lazy<StatusService> _statusService;
        private readonly Lazy<StoragePlatform> _storagePlatform;
        private readonly Lazy<SynchronizePlatform> _synchronizePlatform;
        private readonly List<Timer> _timers = new List<Timer>();
        private readonly Lazy<UploadService> _uploadService;
        private readonly Lazy<WorldService> _worldService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CupCakeMuffinPart{TProtocol}" /> class.
        /// </summary>
        protected CupCakeMuffinPart()
        {
            _name = new Lazy<string>(FindName);

            _connectionPlatform = new Lazy<ConnectionPlatform>(() => PlatformLoader.Get<ConnectionPlatform>());
            _synchronizePlatform =
                new Lazy<SynchronizePlatform>(() => PlatformLoader.Get<SynchronizePlatform>());
            _storagePlatform = new Lazy<StoragePlatform>(() => PlatformLoader.Get<StoragePlatform>());

            _events = new Lazy<EventManager>(() =>
            {
                var eventsPlatform = PlatformLoader.Get<EventsPlatform>();
                return new EventManager(eventsPlatform, this);
            });
            _logger = new Lazy<Logger>(() =>
            {
                var logService = PlatformLoader.Get<LogPlatform>();
                string name = GetName();
                return new Logger(logService, name);
            });

            _chatter = new Lazy<Chatter>(() =>
            {
                var chatService = ServiceLoader.Get<ChatService>();
                string name = GetName();
                return new Chatter(chatService, name);
            });

            _worldService = new Lazy<WorldService>(() => ServiceLoader.Get<WorldService>());
            _roomService = new Lazy<RoomService>(() => ServiceLoader.Get<RoomService>());
            _playerService = new Lazy<PlayerService>(() => ServiceLoader.Get<PlayerService>());
            _keyService = new Lazy<KeyService>(() => ServiceLoader.Get<KeyService>());
            _uploadService = new Lazy<UploadService>(() => ServiceLoader.Get<UploadService>());
            _potionService = new Lazy<PotionService>(() => ServiceLoader.Get<PotionService>());
            _actionService = new Lazy<ActionService>(() => ServiceLoader.Get<ActionService>());
            _messageService = new Lazy<MessageService>(() => ServiceLoader.Get<MessageService>());
            _commandService = new Lazy<CommandService>(() => ServiceLoader.Get<CommandService>());
            _ioService = new Lazy<IOService>(() => ServiceLoader.Get<IOService>());
            _statusService = new Lazy<StatusService>(() => ServiceLoader.Get<StatusService>());
            _permissionService = new Lazy<PermissionService>(() => ServiceLoader.Get<PermissionService>());
        }

        /// <summary>
        ///     Gets the events manager.
        /// </summary>
        /// <value>The events manager.</value>
        protected EventManager Events
        {
            get { return _events.Value; }
        }

        /// <summary>
        ///     Gets the connection platform.
        /// </summary>
        /// <value>The connection platform.</value>
        protected ConnectionPlatform ConnectionPlatform
        {
            get { return _connectionPlatform.Value; }
        }

        /// <summary>
        ///     Gets the synchronize platform.
        /// </summary>
        /// <value>The synchronize platform.</value>
        protected SynchronizePlatform SynchronizePlatform
        {
            get { return _synchronizePlatform.Value; }
        }

        /// <summary>
        ///     Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        protected Logger Logger
        {
            get { return _logger.Value; }
        }

        /// <summary>
        ///     Gets the chatter.
        /// </summary>
        /// <value>The chatter.</value>
        protected Chatter Chatter
        {
            get { return _chatter.Value; }
        }

        /// <summary>
        ///     Gets the world service.
        /// </summary>
        /// <value>The world service.</value>
        protected WorldService WorldService
        {
            get { return _worldService.Value; }
        }

        /// <summary>
        ///     Gets the room service.
        /// </summary>
        /// <value>The room service.</value>
        protected RoomService RoomService
        {
            get { return _roomService.Value; }
        }

        /// <summary>
        ///     Gets the player service.
        /// </summary>
        /// <value>The player service.</value>
        protected PlayerService PlayerService
        {
            get { return _playerService.Value; }
        }

        /// <summary>
        ///     Gets the key service.
        /// </summary>
        /// <value>The key service.</value>
        protected KeyService KeyService
        {
            get { return _keyService.Value; }
        }

        /// <summary>
        ///     Gets the upload service.
        /// </summary>
        /// <value>The upload service.</value>
        protected UploadService UploadService
        {
            get { return _uploadService.Value; }
        }

        /// <summary>
        ///     Gets the potion service.
        /// </summary>
        /// <value>The potion service.</value>
        protected PotionService PotionService
        {
            get { return _potionService.Value; }
        }

        /// <summary>
        ///     Gets the action service.
        /// </summary>
        /// <value>The action service.</value>
        protected ActionService ActionService
        {
            get { return _actionService.Value; }
        }

        /// <summary>
        ///     Gets the message service.
        /// </summary>
        /// <value>The message service.</value>
        protected MessageService MessageService
        {
            get { return _messageService.Value; }
        }

        /// <summary>
        ///     Gets the command service.
        /// </summary>
        /// <value>The command service.</value>
        protected CommandService CommandService
        {
            get { return _commandService.Value; }
        }

        /// <summary>
        ///     Gets the io service.
        /// </summary>
        /// <value>The io service.</value>
        protected IOService IOService
        {
            get { return _ioService.Value; }
        }

        /// <summary>
        ///     Gets the status service.
        /// </summary>
        /// <value>The status service.</value>
        protected StatusService StatusService
        {
            get { return _statusService.Value; }
        }

        /// <summary>
        ///     Gets the permission service.
        /// </summary>
        /// <value>The permission service.</value>
        protected PermissionService PermissionService
        {
            get { return _permissionService.Value; }
        }

        /// <summary>
        ///     Gets the storage platform.
        /// </summary>
        /// <value>The storage platform.</value>
        protected StoragePlatform StoragePlatform
        {
            get { return _storagePlatform.Value; }
        }

        /// <summary>
        ///     Creates a new timer with the given interval, schedules its dispose on this plugin's disable, and returns the timer.
        /// </summary>
        /// <param name="interval">The interval.</param>
        /// <returns>The requested Timer.</returns>
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

        /// <summary>
        ///     Gets the name displayed in chat and log messages.
        /// </summary>
        /// <returns>This plugin's name.</returns>
        protected virtual string GetName()
        {
            return _name.Value;
        }

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
        ///     unmanaged resources.
        /// </param>
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