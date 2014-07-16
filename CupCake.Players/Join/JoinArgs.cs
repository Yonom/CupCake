namespace CupCake.Players.Join
{
    public abstract class JoinArgs
    {
        protected JoinArgs(PlayerService playerService)
        {
            PlayerService = playerService;
        }

        public PlayerService PlayerService { get; private set; }
    }
}