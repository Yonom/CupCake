using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CupCake.Command.Source;

namespace CupCake.Command
{
    /// <summary>
    ///     Class CommandManager.
    /// </summary>
    public sealed class CommandManager : PluginPart, IEnumerable<ICommand>, IDisposable
    {
        private readonly List<ICommand> _commands = new List<ICommand>();
        private readonly object _lockObj = new object(); 

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<ICommand> GetEnumerator()
        {
            lock (this._lockObj)
            {
                return this._commands.ToArray().AsEnumerable().GetEnumerator();
            }
        }

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        ///     Registers the specified command.
        /// </summary>
        /// <param name="callback">The command.</param>
        /// <exception cref="System.ArgumentException">Callback has already been added.</exception>
        public void Add(Action<IInvokeSource, ParsedCommand> callback)
        {
            lock (this._lockObj)
            {
                if (this.ContainsInternal(callback))
                {
                    throw new ArgumentException("Callback has already been added.");
                }

                var command = this.EnablePart<CommandHandle>();
                command.Activate(callback);
                this._commands.Add(command);
            }
        }

        /// <summary>
        ///     Determines whether the specified callback is registered in this <see cref="CommandManager" /> or not.
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
        ///     Removes the specified callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns></returns>
        public bool Remove(Action<IInvokeSource, ParsedCommand> callback)
        {
            ICommand command;
            if (this.TryGetCommand(callback, out command))
            {
                return this.Remove(command);
            }

            return false;
        }

        /// <summary>
        ///     Removes the specified callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <returns></returns>
        public bool Remove(ICommand callback)
        {
            if (this._commands.Remove(callback))
            {
                callback.Dispose();
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Tries to get the command object linked with the specified handler.
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

        public void Dispose()
        {
            lock (this._lockObj)
            {
                foreach (ICommand command in this._commands.ToArray())
                {
                    this.Remove(command);
                }
            }
        }
    }
}