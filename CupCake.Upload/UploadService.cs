using System;
using System.Collections.Generic;
using System.Threading;
using CupCake.Core;
using CupCake.Core.Events;
using CupCake.Messages.Blocks;
using CupCake.Messages.Receive;
using CupCake.Messages.Send;
using CupCake.Messages.User;
using CupCake.Room;
using CupCake.World;

namespace CupCake.Upload
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
            this.ResetUploaded();
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
            IBlockPlaceSendEvent e = request.SendEvent;
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
            IBlockPlaceSendEvent e = request.SendEvent;

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
        private void RaiseEvent(IBlockPlaceSendEvent e)
        {
            var blockEvent = e as BlockPlaceSendEvent;
            if (blockEvent != null)
            {
                this.Events.Raise(blockEvent);
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
            if (this._uploaded[(int)e.Block.Layer, e.Block.X, e.Block.Y])
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
                    IBlockPlaceSendEvent er = request.SendEvent;

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

        private bool IsUploaded(IBlockPlaceSendEvent e)
        {
            return (this._world[e.Layer, e.X, e.Y].IsSame(e));
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
            this._uploaded = new bool[2, this._world.SizeX, this._world.SizeY];
        }

        public void UploadBlock(int x, int y, Block block)
        {
            this.UploadBlock(Layer.Foreground, x, y, block);
        }

        public void UploadBlock(Layer layer, int x, int y, Block block)
        {
            this.Events.Raise(this.GetBlock(layer, x, y, block));
        }

        public void UploadCoinDoor(int x, int y, CoinDoorBlock block, int coinsToCollect)
        {
            this.Events.Raise(this.GetCoinDoor(x, y, block, coinsToCollect));
        }

        public void UploadLabel(int x, int y, LabelBlock block, string text)
        {
            this.Events.Raise(this.GetLabel(x, y, block, text));
        }

        public void UploadPortal(int x, int y, PortalBlock block, int id, int target, PortalRotation rotation)
        {
            this.Events.Raise(this.GetPortal(x, y, block, id, target, rotation));
        }

        public void UploadWorldPortal(int x, int y, WorldPortalBlock block, string roomId)
        {
            this.Events.Raise(this.GetWorldPortal(x, y, block, roomId));
        }

        public void UploadSound(int x, int y, SoundBlock block, int soundId)
        {
            this.Events.Raise(this.GetSound(x, y, block, soundId));
        }

        public void UploadRotatable(int x, int y, RotatableBlock block, int rotation)
        {
            this.Events.Raise(this.GetRotatable(x, y, block, rotation));
        }

        public UploadRequestEvent GetBlock(Layer layer, int x, int y, Block block)
        {
            var e = new BlockPlaceSendEvent(layer, x, y, block);
            return new UploadRequestEvent(e);
        }

        public UploadRequestEvent GetCoinDoor(int x, int y, CoinDoorBlock block, int coinsToCollect)
        {
            var e = new CoinDoorPlaceSendEvent(Layer.Foreground, x, y, block, coinsToCollect);
            return new UploadRequestEvent(e);
        }

        public UploadRequestEvent GetLabel(int x, int y, LabelBlock block, string text)
        {
            var e = new LabelPlaceSendEvent(Layer.Foreground, x, y, block, text);
            return new UploadRequestEvent(e);
        }

        public UploadRequestEvent GetPortal(int x, int y, PortalBlock block, int id, int target,
            PortalRotation rotation)
        {
            var e = new PortalPlaceSendEvent(Layer.Foreground, x, y, block, id, target, rotation);
            return new UploadRequestEvent(e);
        }

        public UploadRequestEvent GetWorldPortal(int x, int y, WorldPortalBlock block, string roomId)
        {
            var e = new WorldPortalPlaceSendEvent(Layer.Foreground, x, y, block, roomId);
            return new UploadRequestEvent(e);
        }

        public UploadRequestEvent GetSound(int x, int y, SoundBlock block, int soundId)
        {
            var e = new SoundPlaceSendEvent(Layer.Foreground, x, y, block, soundId);
            return new UploadRequestEvent(e);
        }

        public UploadRequestEvent GetRotatable(int x, int y, RotatableBlock block, int rotation)
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