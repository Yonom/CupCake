using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.Command;
using CupCake.Command.Source;
using CupCake.Core.Events;
using MuffinFramework.Muffins;

namespace CupCake
{
    /// <summary>
    /// Class CommandManager.
    /// </summary>
    public class CommandManager : IDisposable
    {
        private readonly MuffinArgs _args;
        private readonly string _chatName;
        private readonly List<ICommand> _commands = new List<ICommand>();
        private readonly object _lockObj = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandManager" /> class.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="chatName">The name used in chat output.</param>
        public CommandManager(MuffinArgs args, string chatName)
        {
            this._args = args;
            this._chatName = chatName;
        }

        /// <summary>
        /// Registers the specified callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <exception cref="System.ArgumentException">Callback has already been added.</exception>
        public void Register(Action<IInvokeSource, ParsedCommand> callback)
        {
            lock (this._lockObj)
            {
                if (this.ContainsInternal(callback))
                {
                    throw new ArgumentException("Callback has already been added.");
                }

                var command = new RelayCommand(callback, this._chatName);
                command.Enable(this._args);
                this._commands.Add(command);
            }
        }

        /// <summary>
        /// Determines whether the specified callback is registered in this <see cref="CommandManager"/> or not.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns></returns>
        public bool Contains(Action<IInvokeSource, ParsedCommand> callback)
        {
            lock (this._lockObj)
            {
                return this.ContainsInternal(callback);
            }
        }

        private bool ContainsInternal(Action<IInvokeSource, ParsedCommand> callback)
        {
            return this._commands.Any(c => c.Callback == callback);
        }

        /// <summary>
        /// Removes the specified callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns></returns>
        public bool Remove(Action<IInvokeSource, ParsedCommand> callback)
        {
            ICommand oldCommand;
            if (this.TryGetCommand(callback, out oldCommand))
            {
                oldCommand.Dispose();
                return this._commands.Remove(oldCommand);
            }

            return false;
        }

        /// <summary>
        /// Tries to get the command object linked with the specified handler.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        public bool TryGetCommand(Action<IInvokeSource, ParsedCommand> callback, out ICommand command)
        {
            lock (this._lockObj)
            {
                foreach (ICommand c in 
                    this._commands.Where(c =>
                        c.Callback == callback))
                {
                    command = c;
                    return true;
                }

                command = null;
                return false;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            foreach (var command in this._commands)
            {
                command.Dispose();
            }
        }
    }
}
