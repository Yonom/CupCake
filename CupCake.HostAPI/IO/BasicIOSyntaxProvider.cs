using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.Core.Log;

namespace CupCake.HostAPI.IO
{
    class BasicIOSyntaxProvider : IIOSyntaxProvider
    {
        public string ParseOutput(LogEventArgs e)
        {
            return "[" + e.Priority + "] " + e.Source + ": " + e.Message;
        }

        public string ParseInput(InputEvent e)
        {
            return e.Input;
        }
    }
}
