using System.Collections.Concurrent;
using System.Linq;
using CupCake.Core.Log;
using CupCake.Core.Services;
using CupCake.EE.Events.Receive;
using CupCake.Players.Events;
using CupCake.Players.Join;

namespace CupCake.Players.Services
{
    public class PlayerService : CupCakeService<JoinArgs>
    {
        private readonly ConcurrentDictionary<int, Player> _players = new ConcurrentDictionary<int, Player>();
        public Player CrownPlayer { get; private set; }

        public bool TryGetPlayer(int userId, out Player player)
        {
            return this._players.TryGetValue(userId, out player);
        }

        protected override void Enable()
        {
            this.Events.Bind<AddReceiveEvent>(this.OnAdd);
            this.Events.Bind<CrownReceiveEvent>(this.OnCrown);
            this.Events.Bind<LeftReceiveEvent>(this.OnLeft);
        }

        public Player[] GetPlayers()
        {
            return this._players.Values.ToArray();
        }

        private void OnAdd(object sender, AddReceiveEvent e)
        {
            var player = this.EnablePart<Player>(new AddJoinArgs(this, e));
            if (this._players.TryAdd(player.UserId, player))
            {
                // Raise the add event for this player
                this.Events.Raise(new AddPlayerEvent(player, e));
            }
            else
            {
                // Aww we wasted resources
                player.Dispose();
                this.Logger.Log(LogPriority.Warning, "Received Add with existing UserId. Name: " + e.Username);
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
                this.Logger.Log(LogPriority.Warning, "Received Left with unknown UserId. Name: " + e.UserId);
            }
        }

        public bool Contains(int userId)
        {
            return this._players.ContainsKey(userId);
        }
    }
}