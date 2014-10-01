using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CupCake.Actions;
using CupCake.Chat;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Core;
using CupCake.Core.Events;
using CupCake.Core.Log;
using CupCake.Core.Metadata;
using CupCake.Core.Storage;
using CupCake.HostAPI;
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
        private readonly Lazy<CommandManager> _commands;
        private readonly Lazy<ConnectionPlatform> _connectionPlatform;
        private readonly Lazy<EventManager> _events;
        private readonly Lazy<HostService> _hostService;
        private readonly Lazy<IOService> _ioService;
        private readonly Lazy<KeyService> _keyService;
        private readonly Lazy<Logger> _logger;
        private readonly Lazy<MessageService> _messageService;
        private readonly Lazy<MetadataPlatform> _metadataPlatform;
        private readonly Lazy<string> _name;
        private readonly Lazy<PermissionService> _permissionService;
        private readonly Lazy<PlayerService> _playerService;
        private readonly Lazy<PotionService> _potionService;
        private readonly Lazy<RoomService> _roomService;
        private readonly Lazy<StatusService> _statusService;
        private readonly Lazy<StoragePlatform> _storagePlatform;
        private readonly Lazy<SynchronizePlatform> _synchronizePlatform;
        private readonly Lazy<UploadService> _uploadService;
        private readonly Lazy<WorldService> _worldService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CupCakeMuffinPart{TProtocol}" /> class.
        /// </summary>
        protected CupCakeMuffinPart()
        {
            this._name = new Lazy<string>(this.FindName);

            this._connectionPlatform = new Lazy<ConnectionPlatform>(() => this.PlatformLoader.Get<ConnectionPlatform>());
            this._synchronizePlatform =
                new Lazy<SynchronizePlatform>(() => this.PlatformLoader.Get<SynchronizePlatform>());
            this._storagePlatform = new Lazy<StoragePlatform>(() => this.PlatformLoader.Get<StoragePlatform>());
            this._metadataPlatform = new Lazy<MetadataPlatform>(() => this.PlatformLoader.Get<MetadataPlatform>());

            this._events = new Lazy<EventManager>(() =>
            {
                var eventsPlatform = this.PlatformLoader.Get<EventsPlatform>();
                return new EventManager(eventsPlatform, this);
            });
            this._commands = new Lazy<CommandManager>(() =>
            {
                string name = this.GetName();
                return this.EnablePart<CommandManager, string>(name);
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
            this._roomService = new Lazy<RoomService>(() => this.ServiceLoader.Get<RoomService>());
            this._playerService = new Lazy<PlayerService>(() => this.ServiceLoader.Get<PlayerService>());
            this._keyService = new Lazy<KeyService>(() => this.ServiceLoader.Get<KeyService>());
            this._uploadService = new Lazy<UploadService>(() => this.ServiceLoader.Get<UploadService>());
            this._potionService = new Lazy<PotionService>(() => this.ServiceLoader.Get<PotionService>());
            this._actionService = new Lazy<ActionService>(() => this.ServiceLoader.Get<ActionService>());
            this._messageService = new Lazy<MessageService>(() => this.ServiceLoader.Get<MessageService>());
            this._commandService = new Lazy<CommandService>(() => this.ServiceLoader.Get<CommandService>());
            this._ioService = new Lazy<IOService>(() => this.ServiceLoader.Get<IOService>());
            this._statusService = new Lazy<StatusService>(() => this.ServiceLoader.Get<StatusService>());
            this._permissionService = new Lazy<PermissionService>(() => this.ServiceLoader.Get<PermissionService>());
            this._hostService = new Lazy<HostService>(() => this.ServiceLoader.Get<HostService>());
        }

        /// <summary>
        ///     Gets the events manager.
        /// </summary>
        /// <value>The events manager.</value>
        protected EventManager Events
        {
            get { return this._events.Value; }
        }

        /// <summary>
        ///     Gets the connection platform.
        /// </summary>
        /// <value>The connection platform.</value>
        protected ConnectionPlatform ConnectionPlatform
        {
            get { return this._connectionPlatform.Value; }
        }

        /// <summary>
        ///     Gets the synchronize platform.
        /// </summary>
        /// <value>The synchronize platform.</value>
        protected SynchronizePlatform SynchronizePlatform
        {
            get { return this._synchronizePlatform.Value; }
        }

        /// <summary>
        ///     Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        protected Logger Logger
        {
            get { return this._logger.Value; }
        }

        /// <summary>
        ///     Gets the chatter.
        /// </summary>
        /// <value>The chatter.</value>
        protected Chatter Chatter
        {
            get { return this._chatter.Value; }
        }

        /// <summary>
        ///     Gets the world service.
        /// </summary>
        /// <value>The world service.</value>
        protected WorldService WorldService
        {
            get { return this._worldService.Value; }
        }

        /// <summary>
        ///     Gets the room service.
        /// </summary>
        /// <value>The room service.</value>
        protected RoomService RoomService
        {
            get { return this._roomService.Value; }
        }

        /// <summary>
        ///     Gets the player service.
        /// </summary>
        /// <value>The player service.</value>
        protected PlayerService PlayerService
        {
            get { return this._playerService.Value; }
        }

        /// <summary>
        ///     Gets the key service.
        /// </summary>
        /// <value>The key service.</value>
        protected KeyService KeyService
        {
            get { return this._keyService.Value; }
        }

        /// <summary>
        ///     Gets the upload service.
        /// </summary>
        /// <value>The upload service.</value>
        protected UploadService UploadService
        {
            get { return this._uploadService.Value; }
        }

        /// <summary>
        ///     Gets the potion service.
        /// </summary>
        /// <value>The potion service.</value>
        protected PotionService PotionService
        {
            get { return this._potionService.Value; }
        }

        /// <summary>
        ///     Gets the action service.
        /// </summary>
        /// <value>The action service.</value>
        protected ActionService ActionService
        {
            get { return this._actionService.Value; }
        }

        /// <summary>
        ///     Gets the message service.
        /// </summary>
        /// <value>The message service.</value>
        protected MessageService MessageService
        {
            get { return this._messageService.Value; }
        }

        /// <summary>
        ///     Gets the command service.
        /// </summary>
        /// <value>The command service.</value>
        protected CommandService CommandService
        {
            get { return this._commandService.Value; }
        }

        /// <summary>
        ///     Gets the io service.
        /// </summary>
        /// <value>The io service.</value>
        protected IOService IOService
        {
            get { return this._ioService.Value; }
        }

        /// <summary>
        ///     Gets the status service.
        /// </summary>
        /// <value>The status service.</value>
        protected StatusService StatusService
        {
            get { return this._statusService.Value; }
        }

        /// <summary>
        ///     Gets the permission service.
        /// </summary>
        /// <value>The permission service.</value>
        protected PermissionService PermissionService
        {
            get { return this._permissionService.Value; }
        }

        /// <summary>
        ///     Gets the storage platform.
        /// </summary>
        /// <value>The storage platform.</value>
        protected StoragePlatform StoragePlatform
        {
            get { return this._storagePlatform.Value; }
        }

        /// <summary>
        ///     Gets the metadata platform
        /// </summary>
        /// <value>The metadata platform.</value>
        protected MetadataPlatform MetadataPlatform
        {
            get { return this._metadataPlatform.Value; }
        }

        /// <summary>
        ///     Gets the host service.
        /// </summary>
        /// <value>
        ///     The host service.
        /// </value>
        protected HostService HostService
        {
            get { return this._hostService.Value; }
        }

        /// <summary>
        ///     Gets command service.
        /// </summary>
        /// <value>
        ///     The command service.
        /// </value>
        protected CommandManager Commands
        {
            get { return this._commands.Value; }
        }

        /// <summary>
        ///     Enables the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public override sealed void Enable(MuffinArgs args)
        {
            base.Enable(args);

            MethodInfo[] methods =
                this.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            LayerHelper.LoadEventhandlers(this, this.Events, methods);
            this.LoadCommands(methods);
        }

        private void LoadCommands(IEnumerable<MethodInfo> methods)
        {
            IEnumerable<MethodInfo> eventHandlers = methods.Where(prop => prop.IsDefined(typeof(CommandAttribute), true));
            foreach (MethodInfo eventHandler in eventHandlers)
            {
                ParameterInfo[] parameters = eventHandler.GetParameters();
                if (parameters.Length != 2)
                    throw this.GetCommandEx(eventHandler.Name,
                        "Too few or too many parameters. Command handlers must have exactly two parameters.");

                if (parameters[0].ParameterType != typeof(IInvokeSource))
                    throw this.GetCommandEx(eventHandler.Name, "First argument must be of type IInvokeSource.");
                if (parameters[1].ParameterType != typeof(ParsedCommand))
                    throw this.GetCommandEx(eventHandler.Name, "Second argument must be of type ParsedCommand.");

                var handler =
                    (Action<IInvokeSource, ParsedCommand>)
                        Delegate.CreateDelegate(typeof(Action<IInvokeSource, ParsedCommand>), this, eventHandler);

                this.Commands.Add(handler);
            }
        }

        private Exception GetCommandEx(string name, string reason)
        {
            return
                new TypeLoadException(String.Format("Unable to assign the method {0}.{1} to a command handler. {2}",
                    this.GetType().FullName, name, reason));
        }

        private string FindName()
        {
            var pluginName =
                (PluginNameAttribute)
                    Assembly.GetAssembly(this.GetType())
                        .GetCustomAttributes(typeof(PluginNameAttribute), false)
                        .FirstOrDefault();

            if (pluginName != null)
                return pluginName.Name;

            return this.GetType().Namespace;
        }

        /// <summary>
        ///     Gets the name displayed in chat and log messages.
        /// </summary>
        /// <returns>This plugin's name.</returns>
        protected virtual string GetName()
        {
            return this._name.Value;
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
                if (this._events.IsValueCreated)
                {
                    this._events.Value.Dispose();
                }
                if (this._commands.IsValueCreated)
                {
                    this._commands.Value.Dispose();
                }
            }

            base.Dispose(disposing);
        }
    }
}