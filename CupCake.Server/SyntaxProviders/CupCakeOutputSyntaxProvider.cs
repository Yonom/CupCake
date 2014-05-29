using System;
using CupCake.Core.Log;
using CupCake.Server.Output;

namespace CupCake.Server.SyntaxProviders
{
    public class CupCakeOutputSyntaxProvider : IOutputSyntaxProvider
    {
        public string Parse(LogEventArgs e)
        {
            return "[" + DateTime.Now.ToLongTimeString() + "] (" + e.Priority + ") <" + e.Source + "> " + e.Message;
        }
    }
}