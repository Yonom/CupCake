using System;
using CupCake.Command;
using CupCake.Command.Source;

namespace CupCake
{
    internal class RelayCommand : Command<object>
    {
        private readonly Action<IInvokeSource, ParsedCommand> _innerCommand;
        public RelayCommand(Action<IInvokeSource, ParsedCommand> innerCommand)
        {
            this._innerCommand = innerCommand;
            this.Method = innerCommand.Method;
        }

        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            var handle = this._innerCommand;
            if (handle != null) handle(source, message);
        }
    }
}
