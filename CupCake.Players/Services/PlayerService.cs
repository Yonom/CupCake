using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CupCake.Core.Services;
using CupCake.EE.Events.Receive;
using CupCake.Log;
using CupCake.Log.Services;
using CupCake.Players.Events;
using CupCake.Players.Join;

namespace CupCake.Players.Services
{
    public class PlayerService : CupCakeService<JoinArgs>, IEnumerable<Player>
    {
        private readonly Dictionary<int, Player> _players = new Dictionary<int, Player>();
        public Player CrownPlayer { get; private set; }

        public Player this[int userId]
        {
            get
            {
                lock (this._players)
                {
                    return this._players[userId];
                }
            }
        }

        public IEnumerator<Player> GetEnumerator()
        {
            lock (this._players)
            {
                // Create a new copy of the collection
                List<Player> players = this._players.Values.ToList();
                return players.GetEnumerator();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        protected override void Enable()
        {
            this.Events.Bind<AddReceiveEvent>(this.OnAdd);
            this.Events.Bind<CrownReceiveEvent>(this.OnCrown);
            this.Events.Bind<LeftReceiveEvent>(this.OnLeft);
        }

        private void OnAdd(object sender, AddReceiveEvent e)
        {
            if (!this.Contains(e.UserId))
            {
                var player = this.EnablePart<Player>(new AddJoinArgs(this, e));
                this._players.Add(player.UserId, player);

                // Raise the add event for this player
                this.Events.Raise(new AddPlayerEvent(player, e));
            }
            else
            {
                this.ServiceLoader.Get<LogService>()
                    .Log("PlayerService", LogPriority.Warning, "Received Add with existing UserId. Name: " + e.Username);
            }
        }

        private void OnCrown(object sender, CrownReceiveEvent e)
        {
            if (this.Contains(e.UserId))
            {
                this.CrownPlayer = this[e.UserId];
            }
        }

        private void OnLeft(object sender, LeftReceiveEvent e)
        {
            if (this.Contains(e.UserId))
            {
                this[e.UserId].Dispose();
                this._players.Remove(e.UserId);
            }
        }

        public bool Contains(int userId)
        {
            lock (this._players)
            {
                return this._players.ContainsKey(userId);
            }
        }
    }
}