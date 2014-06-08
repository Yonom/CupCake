using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.DefaultCommands.Commands;
using CupCake.DefaultCommands.Commands.Utility;

namespace CupCake.DefaultCommands
{
    class UtilityCommandsMuffin : CupCakeMuffin<UtilityCommandsMuffin>
    {
        protected override void Enable()
        {
            this.EnablePart<HelpCommand>();
        }
    }
}
