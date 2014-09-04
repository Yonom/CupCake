using System;
using CupCake.Command;
using CupCake.Command.Source;

namespace CupCake
{
    internal class RelayCommand : Command<object>, ICommand
    {
        public Action<IInvokeSource, ParsedCommand> Callback { get; private set; }
        private readonly string _name;

        public RelayCommand(Action<IInvokeSource, ParsedCommand> callback, string name)
        {
            this.Callback = callback;
            this._name = name;
            this.Method = callback.Method;
        }

        protected override string GetName()
        {
            return _name;
        }

        protected override void Run(IInvokeSource source, ParsedCommand message)
        {
            this.Callback(source, message);
        }
    }
}