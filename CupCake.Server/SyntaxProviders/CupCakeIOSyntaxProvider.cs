using System;
using CupCake.Core.Log;
using CupCake.Server.IO;

namespace CupCake.Server.SyntaxProviders
{
    public class CupCakeIOSyntaxProvider : IOutputSyntaxProvider
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