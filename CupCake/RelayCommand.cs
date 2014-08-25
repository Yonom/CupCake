using System;
using CupCake.Command;
using CupCake.Command.Source;

namespace CupCake
{
    internal class RelayCommand : Command<object>
    {
        private readonly Action<IInvokeSource, ParsedCommand> _innerCommand;
        private readonly string _name;

        public RelayCommand(Action<IInvokeSource, ParsedCommand> innerCommand, string name)
        {
            this._innerCommand = innerCommand;
            this._name = name;
            this.Method = innerCommand.Method;
        }

        protected override string GetName()
        {
            return _name;
        }

        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            Action<IInvokeSource, ParsedCommand> handle = this._innerCommand;
            if (handle != null) handle(source, message);
        }
    }
}