using System;
using System.Collections.Concurrent;
using System.Linq;
using CupCake.Core;
using CupCake.Core.Events;
using CupCake.Core.Log;
using CupCake.Messages.Receive;
using CupCake.Players.Join;

namespace CupCake.Players
{
    public sealed class PlayerService : CupCakeService<JoinArgs>
    {
        private readonly ConcurrentDictionary<int, Player> _players = new ConcurrentDictionary<int, Player>();
        public Player OwnPlayer { get; private set; }
        public Player CrownPlayer { get; private set; }

        public Player[] Players
        {
            get { return _players.Values.ToArray(); }
        }

        public int Count
        {
            get { return _players.Count; }
        }

        public bool TryGetPlayer(int userId, out Player player)
        {
            return _players.TryGetValue(userId, out player);
        }

        protected override void Enable()
        {
            Events.Bind<CrownReceiveEvent>(OnCrown, EventPriority.High);
            Events.Bind<LeftReceiveEvent>(OnLeft, EventPriority.High);
            Events.Bind<AddReceiveEvent>(OnAdd, EventPriority.High);
            Events.Bind<InitReceiveEvent>(OnInit, EventPriority.High);
        }

        private void OnInit(object sender, InitReceiveEvent e)
        {
            AddPlayer(new InitJoinArgs(this, e), e, player =>
                OwnPlayer = player);
        }

        private void OnAdd(object sender, AddReceiveEvent e)
        {
            AddPlayer(new AddJoinArgs(this, e), e,
                player =>
                    // Raise the add event for this player
                    Events.Raise(new AddPlayerEvent(player, e)),
                () =>
                    Logger.Log(LogPriority.Warning, "Received Add with existing UserId. Name: " + e.Username));
        }

        private void AddPlayer(JoinArgs joinArgs, IUserPosReceiveEvent e, Action<Player> successCallback = null,
            Action errorcallback = null)
        {
            var player = EnablePart<Player>(joinArgs);
            if (_players.TryAdd(player.UserId, player))
            {
                if (successCallback != null)
                    successCallback(player);

                Events.Raise(new JoinPlayerEvent(player, e));
            }
            else
            {
                // Aww we wasted resources
                player.Dispose();

                if (errorcallback != null)
                    errorcallback();
            }
        }

        private void OnCrown(object sender, CrownReceiveEvent e)
        {
            Player player;
            if (TryGetPlayer(e.UserId, out player))
            {
                CrownPlayer = player;
            }
        }

        private void OnLeft(object sender, LeftReceiveEvent e)
        {
            Player leftPlayer;
            if (_players.TryRemove(e.UserId, out leftPlayer))
            {
                leftPlayer.Dispose();
            }
            else
            {
                Logger.Log(LogPriority.Warning, "Received Left with unknown UserId. UserId: " + e.UserId);
            }
        }

        public bool Contains(int userId)
        {
            return _players.ContainsKey(userId);
        }
    }
}