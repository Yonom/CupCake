using System.Diagnostics;
using CupCake.Core;
using CupCake.Core.Events;
using CupCake.Core.Log;

namespace CupCake.HostAPI.IO
{
    public class IOService : CupCakeService
    {
        public IIOSyntaxProvider SyntaxProvider { get; set; }
        public LogPriority MinPriority { get; set; }

        protected override void Enable()
        {
            this.SyntaxProvider = new BasicIOSyntaxProvider();

            if (Debugger.IsAttached)
                this.MinPriority = LogPriority.Debug;

            this.Logger.LogPlatform.LogReceived += this.LogPlatform_LogReceived;
            this.Events.Bind<InputEvent>(this.OnInput, EventPriority.High);
        }

        private void OnInput(object sender, InputEvent e)
        {
            string input = this.SyntaxProvider.ParseInput(e);
            var outputEvent = new OutputEvent(input);
            this.Events.Raise(outputEvent);
        }

        private void LogPlatform_LogReceived(object sender, LogEventArgs e)
        {
            if (e.Priority >= this.MinPriority)
            {
                string output = this.SyntaxProvider.ParseOutput(e);
                var outputEvent = new OutputEvent(output);
                this.Events.Raise(outputEvent);
            }
        }
    }
}