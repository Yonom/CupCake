using System;
using System.Collections.Generic;
using System.Threading;
using CupCake.Core.Events;
using CupCake.Core.Services;
using CupCake.Messages.Blocks;
using CupCake.Messages.Events.Receive;
using CupCake.Messages.Events.Send;
using CupCake.Messages.User;
using CupCake.Room.Events;
using CupCake.Room.Services;
using CupCake.World.Events;
using CupCake.World.Services;

namespace CupCake.Upload.Services
{
    public class UploadService : CupCakeService
    {
        private readonly Queue<UploadRequestEvent> _checkQueue = new Queue<UploadRequestEvent>();
        private RoomService _room;
        private bool[,,] _uploaded;
        private DequeWorker _workThread;
        private WorldService _world;

        protected override void Enable()
        {
            this.ServiceLoader.EnableComplete += this.ServiceLoader_EnableComplete;

            this._workThread = new DequeWorker();

            this.Events.Bind<UploadRequestEvent>(this.OnUploadRequest, EventPriority.Lowest);
            this.Events.Bind<BlockPlaceEvent>(this.OnBlockPlace);
            this.Events.Bind<InitCompleteEvent>(this.OnInitComplete);
            this.Events.Bind<AccessRightChangeEvent>(this.OnAccessRightChange);
            this.Events.Bind<ClearReceiveEvent>(this.OnClear);
            this.Events.Bind<ResetReceiveEvent>(this.OnReset);
        }

        private void OnReset(object sender, ResetReceiveEvent e)
        {
            this.Reset();
        }

        private void OnClear(object sender, ClearReceiveEvent e)
        {
            this.Reset();
        }

        private void OnAccessRightChange(object sender, AccessRightChangeEvent e)
        {
            if (this._room.AccessRight >= AccessRight.Edit)
            {
                this._workThread.Start();
            }
            else
            {
                this._workThread.Stop();
            }
        }

        private void ServiceLoader_EnableComplete(object sender, EventArgs e)
        {
            this._world = this.ServiceLoader.Get<WorldService>();
            this._room = this.ServiceLoader.Get<RoomService>();
        }

        private void OnInitComplete(object sender, InitCompleteEvent e)
        {
            this._uploaded = new bool[1, this._world.SizeX, this._world.SizeY];
        }

        private void OnUploadRequest(object sender, UploadRequestEvent e)
        {
            Action task = () =>
            {
                if (this.Send(e))
                {
                    Thread.Sleep(10);
                }

                if (this._workThread.Count == 0)
                {
                    ThreadPool.QueueUserWorkItem(o =>
                    {
                        Thread.Sleep(1000);
                        this.DoLagCheck(true);
                    });
                }
            };

            if (e.IsUrgent)
            {
                this._workThread.QueueFront(task);
            }
            else
            {
                this._workThread.QueueBack(task);
            }
        }

        /// <summary>
        ///     Uploads the given block and makes sure it gets placed
        /// </summary>
        /// <param name="request"></param>
        private bool Send(UploadRequestEvent request)
        {
            BlockPlaceSendEvent e = request.SendEvent;
            e.Encryption = this._room.Encryption;

            request.SendTries++;
            // If not block already exists
            if (!this.IsUploaded(e))
            {
                this.RaiseEvent(e);
                this.EnqueueCheck(request);
                return true;
            }

            request.Verified = true;
            return false;
        }

        /// <summary>
        ///     Adds the request to the lag checking queue
        /// </summary>
        /// <param name="request"></param>
        private void EnqueueCheck(UploadRequestEvent request)
        {
            BlockPlaceSendEvent e = request.SendEvent;

            lock (this._checkQueue)
            {
                if (!this._uploaded[(int)e.Layer, e.X, e.Y] && request.SendTries < 5)
                {
                    this._uploaded[(int)e.Layer, e.X, e.Y] = true;

                    this._checkQueue.Enqueue(request);
                }
            }
        }

        /// <summary>
        ///     Calls the correct event for the given BlockPlaceSendEvent
        /// </summary>
        /// <param name="e"></param>
        private void RaiseEvent(BlockPlaceSendEvent e)
        {
            if (e.GetType() == typeof(BlockPlaceSendEvent))
            {
                this.Events.Raise(e);
                return;
            }

            var coinEvent = e as CoinDoorPlaceSendEvent;
            if (coinEvent != null)
            {
                this.Events.Raise(coinEvent);
                return;
            }
            var labelEvent = e as LabelPlaceSendEvent;
            if (labelEvent != null)
            {
                this.Events.Raise(labelEvent);
                return;
            }
            var worldPortalEvent = e as WorldPortalPlaceSendEvent;
            if (worldPortalEvent != null)
            {
                this.Events.Raise(worldPortalEvent);
                return;
            }
            var rotatableEvent = e as RotatablePlaceSendEvent;
            if (rotatableEvent != null)
            {
                this.Events.Raise(rotatableEvent);
                return;
            }
            var soundEvent = e as SoundPlaceSendEvent;
            if (soundEvent != null)
            {
                this.Events.Raise(soundEvent);
                return;
            }
            var portalEvent = e as PortalPlaceSendEvent;
            if (portalEvent != null)
            {
                this.Events.Raise(portalEvent);
            }
        }

        private void OnBlockPlace(object sender, BlockPlaceEvent e)
        {
            if (this._uploaded[(int)e.Layer, e.PosX, e.PosY])
            {
                this.DoLagCheck(false);
            }
        }

        private void DoLagCheck(bool emptyQueue)
        {
            lock (this._checkQueue)
            {
                while (this._checkQueue.Count > 0)
                {
                    UploadRequestEvent request = this._checkQueue.Dequeue();
                    BlockPlaceSendEvent er = request.SendEvent;

                    this._uploaded[(int)er.Layer, er.X, er.Y] = false;
                    if (this.IsUploaded(er))
                    {
                        request.Verified = true;

                        if (emptyQueue)
                            continue;

                        break;
                    }

                    request.IsUrgent = true;
                    this.Events.Raise(request);
                }
            }
        }

        private bool IsUploaded(BlockPlaceSendEvent e)
        {
            return (this._world[e.Layer, e.X, e.Y] == e);
        }

        public void ClearQueue()
        {
            this._workThread.Clear();
            this.DoLagCheck(true);
        }

        private void Reset()
        {
            this._workThread.Stop();
            this.ClearQueue();
            this.ResetUploaded();
            this._workThread.Clear();
            this._workThread.Start();
        }

        private void ResetUploaded()
        {
            this._uploaded = new bool[1, this._world.SizeX, this._world.SizeY];
        }

        public UploadRequestEvent UploadBlock(int x, int y, Block block)
        {
            return this.UploadBlock(Layer.Foreground, x, y, block);
        }

        public UploadRequestEvent UploadBlock(Layer layer, int x, int y, Block block)
        {
            var e = new BlockPlaceSendEvent(layer, x, y, block);
            return new UploadRequestEvent(e);
        }

        public UploadRequestEvent UploadCoinDoor(int x, int y, CoinDoorBlock block, int coinsToCollect)
        {
            var e = new CoinDoorPlaceSendEvent(Layer.Foreground, x, y, block, coinsToCollect);
            return new UploadRequestEvent(e);
        }

        public UploadRequestEvent UploadLabel(int x, int y, LabelBlock block, string text)
        {
            var e = new LabelPlaceSendEvent(Layer.Foreground, x, y, block, text);
            return new UploadRequestEvent(e);
        }

        public UploadRequestEvent UploadPortal(int x, int y, PortalBlock block, int id, int target,
            PortalRotation rotation)
        {
            var e = new PortalPlaceSendEvent(Layer.Foreground, x, y, block, id, target, rotation);
            return new UploadRequestEvent(e);
        }

        public UploadRequestEvent UploadWorldPortal(int x, int y, WorldPortalBlock block, string roomId)
        {
            var e = new WorldPortalPlaceSendEvent(Layer.Foreground, x, y, block, roomId);
            return new UploadRequestEvent(e);
        }

        public UploadRequestEvent UploadSound(int x, int y, SoundBlock block, int soundId)
        {
            var e = new SoundPlaceSendEvent(Layer.Foreground, x, y, block, soundId);
            return new UploadRequestEvent(e);
        }

        public UploadRequestEvent UploadRotatable(int x, int y, RotatableBlock block, int rotation)
        {
            var e = new RotatablePlaceSendEvent(Layer.Foreground, x, y, block, rotation);
            return new UploadRequestEvent(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._workThread.Stop();
            }

            base.Dispose(disposing);
        }
    }
}