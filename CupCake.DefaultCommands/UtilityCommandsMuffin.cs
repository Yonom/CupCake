using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.DefaultCommands.Commands;
using CupCake.DefaultCommands.Commands.Utility;

namespace CupCake.DefaultCommands
{
    public class UtilityCommandsMuffin : CupCakeMuffin<UtilityCommandsMuffin>
    {
        protected override void Enable()
        {
            this.EnablePart<PingCommand>();
            this.EnablePart<HiCommand>();
            this.EnablePart<HelpCommand>();
            this.EnablePart<SayCommand>();
            this.EnablePart<SayRawCommand>();
        }
    }
}
