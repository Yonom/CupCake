using System;
using CupCake.Core.Log;

namespace CupCake.HostAPI.IO
{
    public class CupCakeIOSyntaxProvider : IIOSyntaxProvider
    {
        public string ParseOutput(LogEventArgs e)
        {
            return "[" + DateTime.Now.ToLongTimeString() + "] (" + e.Priority + ") <" + e.Source + "> " + e.Message;
        }

        public string ParseInput(InputEvent e)
        {
            return "[" + DateTime.Now.ToLongTimeString() + "] > " + e.Input;
        }
    }
}