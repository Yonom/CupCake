﻿using System;
using System.Collections.Concurrent;
using System.Linq;
using CupCake.Core;
using CupCake.Core.Events;
using CupCake.Core.Log;
using CupCake.Messages.Receive;
using CupCake.Players.Join;

namespace CupCake.Players
{
    public class PlayerService : CupCakeService<JoinArgs>
    {
        private readonly ConcurrentDictionary<int, Player> _players = new ConcurrentDictionary<int, Player>();
        public Player OwnPlayer { get; private set; }
        public Player CrownPlayer { get; private set; }

        public Player[] Players
        {
            get { return this._players.Values.ToArray(); }
        }

        public int Count {
            get { return this._players.Count; }
        }

        public bool TryGetPlayer(int userId, out Player player)
        {
            return this._players.TryGetValue(userId, out player);
        }

        protected override void Enable()
        {
            this.Events.Bind<AddReceiveEvent>(this.OnAdd, EventPriority.Lowest);
            this.Events.Bind<InitReceiveEvent>(this.OnInit, EventPriority.Low);
            this.Events.Bind<CrownReceiveEvent>(this.OnCrown, EventPriority.Low);
            this.Events.Bind<LeftReceiveEvent>(this.OnLeft, EventPriority.Low);
        }

        private void OnInit(object sender, InitReceiveEvent e)
        {
            this.AddPlayer(new InitJoinArgs(this, e), player =>
                this.OwnPlayer = player);
        }

        private void OnAdd(object sender, AddReceiveEvent e)
        {
            this.AddPlayer(new AddJoinArgs(this, e),
                player =>
                    // Raise the add event for this player
                    this.Events.Raise(new AddPlayerEvent(player, e)),
                () =>
                    this.Logger.Log(LogPriority.Warning, "Received Add with existing UserId. Name: " + e.Username));
        }

        private void AddPlayer(JoinArgs joinArgs, Action<Player> successCallback = null, Action errorcallback = null)
        {
            var player = this.EnablePart<Player>(joinArgs);
            if (this._players.TryAdd(player.UserId, player))
            {
                if (successCallback != null)
                    successCallback(player);
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
            if (this.TryGetPlayer(e.UserId, out player))
            {
                this.CrownPlayer = player;
            }
        }

        private void OnLeft(object sender, LeftReceiveEvent e)
        {
            Player leftPlayer;
            if (this._players.TryRemove(e.UserId, out leftPlayer))
            {
                leftPlayer.Dispose();
            }
            else
            {
                this.Logger.Log(LogPriority.Warning, "Received Left with unknown UserId. UserId: " + e.UserId);
            }
        }

        public bool Contains(int userId)
        {
            return this._players.ContainsKey(userId);
        }
    }
}