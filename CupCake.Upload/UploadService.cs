using System;
using System.Linq;
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
    public sealed class UploadService : CupCakeService
    {
        private readonly Deque<UploadRequestEvent> _checkQueue = new Deque<UploadRequestEvent>();
        private int[,,] _uploaded;
        private DequeWorker _workThread;
        private WorldService _world;

        public int Count
        {
            get { return _workThread.Count; }
        }

        public int LagCheckCount
        {
            get { return _checkQueue.Count; }
        }

        protected override void Enable()
        {
            ServiceLoader.EnableComplete += ServiceLoader_EnableComplete;

            _workThread = new DequeWorker();

            Events.Bind<UploadRequestEvent>(OnUploadRequest, EventPriority.Lowest);

            Events.Bind<PlaceWorldEvent>(OnBlockPlace, EventPriority.High);
            Events.Bind<InitReceiveEvent>(OnInitComplete, EventPriority.High);
            Events.Bind<AccessRoomEvent>(OnAccessRightChange, EventPriority.High);
            Events.Bind<ClearReceiveEvent>(OnClear, EventPriority.High);
            Events.Bind<ResetReceiveEvent>(OnReset, EventPriority.High);
        }

        private void OnReset(object sender, ResetReceiveEvent e)
        {
            Reset(_world.RoomWidth, _world.RoomHeight);
        }

        private void OnClear(object sender, ClearReceiveEvent e)
        {
            Reset(e.RoomWidth, e.RoomHeight);
        }

        private void OnAccessRightChange(object sender, AccessRoomEvent e)
        {
            if (e.NewRights >= AccessRight.Edit)
            {
                _workThread.Start();
            }
            else
            {
                _workThread.Stop();
            }
        }

        private void ServiceLoader_EnableComplete(object sender, EventArgs e)
        {
            _world = ServiceLoader.Get<WorldService>();
            ServiceLoader.Get<RoomService>();
        }

        private void OnInitComplete(object sender, InitReceiveEvent e)
        {
            ResetUploaded(e.RoomWidth, e.RoomHeight);
        }

        private void OnUploadRequest(object sender, UploadRequestEvent e)
        {
            Action task = () =>
            {
                if (Send(e))
                {
                    Thread.Sleep(10);
                }

                if (_workThread.Count == 0)
                {
                    ThreadPool.QueueUserWorkItem(o =>
                    {
                        Thread.Sleep(1000);
                        DoLagCheck(true);
                    });
                }
            };

            if (e.IsUrgent)
            {
                _workThread.QueueFront(task);
            }
            else
            {
                _workThread.QueueBack(task);
            }
        }

        /// <summary>
        ///     Uploads the given block and makes sure it gets placed
        /// </summary>
        /// <param name="request"></param>
        private bool Send(UploadRequestEvent request)
        {
            IBlockPlaceSendEvent e = request.SendEvent;

            request.SendTries++;
            // If not block already exists
            if (!IsUploaded(e))
            {
                RaiseEvent(e);
                EnqueueCheck(request);
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

            lock (_checkQueue)
            {
                if (request.SendTries < 5)
                {
                    // If there already was a block in the lag checks, remove it, add this one instead
                    if (_uploaded[(int) e.Layer, e.X, e.Y] > 0)
                    {
                        UploadRequestEvent prev =
                            _checkQueue.FirstOrDefault(
                                req =>
                                    req.SendEvent.Layer == e.Layer && req.SendEvent.X == e.X && req.SendEvent.Y == e.Y);
                        if (prev != null)
                        {
                            _checkQueue.Remove(prev);
                        }
                    }
                    _uploaded[(int) e.Layer, e.X, e.Y]++;

                    _checkQueue.AddToBack(request);
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
                Events.Raise(blockEvent);
                return;
            }

            var coinEvent = e as CoinDoorPlaceSendEvent;
            if (coinEvent != null)
            {
                Events.Raise(coinEvent);
                return;
            }
            var labelEvent = e as LabelPlaceSendEvent;
            if (labelEvent != null)
            {
                Events.Raise(labelEvent);
                return;
            }
            var worldPortalEvent = e as WorldPortalPlaceSendEvent;
            if (worldPortalEvent != null)
            {
                Events.Raise(worldPortalEvent);
                return;
            }
            var rotatableEvent = e as RotatablePlaceSendEvent;
            if (rotatableEvent != null)
            {
                Events.Raise(rotatableEvent);
                return;
            }
            var soundEvent = e as SoundPlaceSendEvent;
            if (soundEvent != null)
            {
                Events.Raise(soundEvent);
                return;
            }
            var portalEvent = e as PortalPlaceSendEvent;
            if (portalEvent != null)
            {
                Events.Raise(portalEvent);
            }
        }

        private void OnBlockPlace(object sender, PlaceWorldEvent e)
        {
            // If there has been a block uploaded at this position
            if (_uploaded[(int) e.WorldBlock.Layer, e.WorldBlock.X, e.WorldBlock.Y] > 0)
            {
                // If this is the last block being uploaded to this position
                if (--_uploaded[(int) e.WorldBlock.Layer, e.WorldBlock.X, e.WorldBlock.Y] == 0)
                {
                    DoLagCheck(false);
                }
            }
        }

        private void DoLagCheck(bool emptyQueue)
        {
            lock (_checkQueue)
            {
                while (_checkQueue.Count > 0)
                {
                    UploadRequestEvent request = _checkQueue.RemoveFromFront();
                    IBlockPlaceSendEvent er = request.SendEvent;

                    _uploaded[(int) er.Layer, er.X, er.Y] = 0;
                    if (IsUploaded(er))
                    {
                        request.Verified = true;

                        if (emptyQueue)
                            continue;

                        break;
                    }

                    request.IsUrgent = true;
                    Events.Raise(request);
                }

                if (_workThread.Count == 0)
                {
                    Events.Raise(new FinishUploadEvent());
                }
            }
        }

        private bool IsUploaded(IBlockPlaceSendEvent e)
        {
            return (_world[e.Layer, e.X, e.Y].IsSame(e));
        }

        public void ClearQueue()
        {
            _workThread.Clear();
            DoLagCheck(true);
        }

        private void Reset(int width, int height)
        {
            _workThread.Stop();
            ClearQueue();
            ResetUploaded(width, height);
            _workThread.Clear();
            _workThread.Start();
        }

        private void ResetUploaded(int width, int height)
        {
            _uploaded = new int[2, width, height];
        }

        public void UploadBlock(int x, int y, Block block)
        {
            UploadBlock(Layer.Foreground, x, y, block);
        }

        public void UploadBlock(Layer layer, int x, int y, Block block)
        {
            Events.Raise(GetBlock(layer, x, y, block));
        }

        public void UploadCoinDoor(int x, int y, CoinDoorBlock block, int coinsToCollect)
        {
            Events.Raise(GetCoinDoor(x, y, block, coinsToCollect));
        }

        public void UploadLabel(int x, int y, LabelBlock block, string text)
        {
            Events.Raise(GetLabel(x, y, block, text));
        }

        public void UploadPortal(int x, int y, PortalBlock block, int id, int target, PortalRotation rotation)
        {
            Events.Raise(GetPortal(x, y, block, id, target, rotation));
        }

        public void UploadWorldPortal(int x, int y, WorldPortalBlock block, string roomId)
        {
            Events.Raise(GetWorldPortal(x, y, block, roomId));
        }

        public void UploadSound(int x, int y, SoundBlock block, int soundId)
        {
            Events.Raise(GetSound(x, y, block, soundId));
        }

        public void UploadRotatable(int x, int y, RotatableBlock block, int rotation)
        {
            Events.Raise(GetRotatable(x, y, block, rotation));
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
                _workThread.Stop();
            }

            base.Dispose(disposing);
        }
    }
}