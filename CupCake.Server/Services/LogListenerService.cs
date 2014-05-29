using System.Diagnostics;
using CupCake.Core.Log;
using CupCake.Core.Services;
using CupCake.Server.Output;
using CupCake.Server.SyntaxProviders;

namespace CupCake.Server.Services
{
    public class LogListenerService : CupCakeService
    {
        public IOutputSyntaxProvider SyntaxProvider { get; set; }
        public LogPriority MinPriority { get; set; }

        protected override void Enable()
        {
            this.SyntaxProvider = new CupCakeOutputSyntaxProvider();

            if (Debugger.IsAttached)
                this.MinPriority = LogPriority.Debug;

            this.Logger.LogPlatform.LogReceived += this.LogPlatform_LogReceived;
        }

        private void LogPlatform_LogReceived(object sender, LogEventArgs e)
        {
            if (e.Priority >= this.MinPriority)
            {
                string output = this.SyntaxProvider.Parse(e);
                var outputEvent = new CupCakeOutputEvent(output);
                this.Events.Raise(outputEvent);
            }
        }
    }
}