using System;
using CupCake.Core.Log;
using CupCake.HostAPI.IO;

namespace CupCake.Server.SyntaxProviders
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