using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.DefaultCommands.Commands;

namespace CupCake.DefaultCommands
{
    public class DefaultCommandsMuffin : CupCakeMuffin<DefaultCommandsMuffin>
    {
        protected override void Enable()
        {
            this.EnablePart<KickCommand>();
        }
    }
}
