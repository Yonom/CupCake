using System;
using System.Collections.Generic;
using CupCake.Actions.Services;
using CupCake.Chat.Services;
using CupCake.Core.Events;
using CupCake.Core.Log;
using CupCake.Core.Platforms;
using CupCake.Keys.Services;
using CupCake.Players.Services;
using CupCake.Potions.Services;
using CupCake.Room.Events;
using CupCake.Room.Services;
using CupCake.Upload.Services;
using CupCake.World.Services;
using MuffinFramework;
using MuffinFramework.Muffins;

namespace CupCake.API.Muffins
{
    public abstract class CupCakeMuffinPart<TProtocol> : MuffinPart<TProtocol>
    {
        private readonly Lazy<Chatter> _chatter;
        private readonly Lazy<ConnectionPlatform> _connectionPlatform;
        private readonly Lazy<EventManager> _events;
        private readonly Lazy<KeyService> _keyService;
        private readonly Lazy<Logger> _logger;
        private readonly Lazy<PlayerService> _playerService;
        private readonly Lazy<RoomService> _roomService;
        private readonly Lazy<UploadService> _uploadService;
        private readonly Lazy<WorldService> _worldService;
        private readonly Lazy<PotionService> _potionService;
        private readonly Lazy<ActionService> _actionService;

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
            this._roomService = new Lazy<RoomService>(() => this.ServiceLoader.Get<RoomService>());
            this._playerService = new Lazy<PlayerService>(() => this.ServiceLoader.Get<PlayerService>());
            this._keyService = new Lazy<KeyService>(() => this.ServiceLoader.Get<KeyService>());
            this._uploadService = new Lazy<UploadService>(() => this.ServiceLoader.Get<UploadService>());
            this._potionService = new Lazy<PotionService>(() => this.ServiceLoader.Get<PotionService>());
            this._actionService = new Lazy<ActionService>(() => this.ServiceLoader.Get<ActionService>());
        }

        protected EventManager Events
        {
            get { return this._events.Value; }
        }

        protected ConnectionPlatform ConnectionPlatform
        {
            get { return this._connectionPlatform.Value; }
        }

        protected Logger Logger
        {
            get { return this._logger.Value; }
        }

        protected Chatter Chatter
        {
            get { return this._chatter.Value; }
        }

        protected WorldService WorldService
        {
            get { return this._worldService.Value; }
        }

        protected RoomService RoomService
        {
            get { return this._roomService.Value; }
        }

        protected PlayerService PlayerService
        {
            get { return this._playerService.Value; }
        }

        protected KeyService KeyService
        {
            get { return this._keyService.Value; }
        }

        protected UploadService UploadService
        {
            get { return this._uploadService.Value; }
        }

        protected PotionService PotionService
        {
            get { return this._potionService.Value; }
        }

        protected ActionService ActionService
        {
            get { return this._actionService.Value; }
        }

        protected virtual string GetName()
        {
            return this.GetType().Namespace;
        }

        //protected TPart EnableCommand<TPart, TCommmandProtocol>(TCommmandProtocol host) where TPart : class, ILayerPart<TCommmandProtocol, CommandArgs>, new()
        //{
        //    var part = new TPart();
        //    part.Enable(host, new CommandArgs(this.PlatformLoader, this.ServiceLoader, this.MuffinLoader));

        //    lock (this._lockObj)
        //    {
        //        this._commandParts.Add(part);
        //    }

        //    return part;
        //}

        //protected TPart EnableCommand<TPart, TCommmandProtocol>() where TPart : class, ILayerPart<TCommmandProtocol, CommandArgs>, new()
        //{
        //    var host = (TCommmandProtocol)(object)this;
        //    return this.EnableCommand<TPart, TCommmandProtocol>(host);
        //}

        //protected TPart EnableCommand<TPart>(TProtocol host) where TPart : class, ILayerPart<TProtocol, CommandArgs>, new()
        //{
        //    return this.EnableCommand<TPart, TProtocol>(host);
        //}

        //protected TPart EnableCommand<TPart>() where TPart : class, ILayerPart<TProtocol, CommandArgs>, new()
        //{
        //    return this.EnableCommand<TPart, TProtocol>();
        //}

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