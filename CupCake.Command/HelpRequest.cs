﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CupCake.Command
{
    public class HelpRequest : ParsedCommand
    {
        public HelpRequest(string source)
            : base(source)
        {
        }
    }
}
