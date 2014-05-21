using CupCake.Players.Services;

namespace CupCake.Players.Join
{
    public abstract class JoinArgs
    {
        protected JoinArgs(PlayerService playerService)
        {
            this.PlayerService = playerService;
        }

        public PlayerService PlayerService { get; private set; }
    }
}