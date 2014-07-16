using System;
using System.Diagnostics;
using System.Linq;

namespace CupCake.Command
{
    [DebuggerDisplay("Source = {Source}")]
    public class ParsedCommand
    {
        public ParsedCommand(string source)
        {
            Source = source;
            string[] parts = source.Split(' ');
            Type = parts[0];
            Args = parts.Skip(1).ToArray();
        }

        public string Source { get; private set; }

        public string Type { get; private set; }
        public string[] Args { get; private set; }

        public int Count
        {
            get { return Args.Length; }
        }

        public int GetInt(int index)
        {
            try
            {
                return Convert.ToInt32(Args[index]);
            }
            catch (FormatException)
            {
                throw new CommandException("Could not convert parameter " + index + " to integer.");
            }
            catch (OverflowException)
            {
                throw new CommandException("Integer at parameter " + index + " was too big.");
            }
        }

        public string GetTrail(int index)
        {
            return String.Join(" ", Args.Skip(index));
        }
    }
}