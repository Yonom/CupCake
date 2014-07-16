using System.Diagnostics;
using CupCake.Core;
using CupCake.Core.Events;
using CupCake.Core.Log;

namespace CupCake.HostAPI.IO
{
    public sealed class IOService : CupCakeService
    {
        public IIOSyntaxProvider SyntaxProvider { get; set; }
        public LogPriority MinPriority { get; set; }

        protected override void Enable()
        {
            SyntaxProvider = new BasicIOSyntaxProvider();

            if (Debugger.IsAttached)
                MinPriority = LogPriority.Debug;

            Logger.LogPlatform.LogReceived += LogPlatform_LogReceived;
            Events.Bind<InputEvent>(OnInput, EventPriority.High);
        }

        private void OnInput(object sender, InputEvent e)
        {
            string input = SyntaxProvider.ParseInput(e);
            var outputEvent = new OutputEvent(input);
            Events.Raise(outputEvent);
        }

        private void LogPlatform_LogReceived(object sender, LogEventArgs e)
        {
            if (e.Priority >= MinPriority)
            {
                string output = SyntaxProvider.ParseOutput(e);
                var outputEvent = new OutputEvent(output);
                Events.Raise(outputEvent);
            }
        }
    }
}