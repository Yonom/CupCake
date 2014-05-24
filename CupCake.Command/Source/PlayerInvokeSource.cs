using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.API;
using CupCake.Players;

namespace CupCake.Command.Source
{
    public class PlayerInvokeSource : IInvokeSource
    {
        private readonly Chatter _chatter;

        public Player Player { get; private set; }

        public bool Handled { get; set; }

        public PlayerInvokeSource(Player player, Chatter chatter)
        {
            this._chatter = chatter;
            this.Player = player;
        }

        public void Reply(string message)
        {
            this._chatter.Reply(Player.Username, message);   
        }
    }
}
