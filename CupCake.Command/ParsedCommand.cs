using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CupCake.Command
{
    public class ParsedCommand
    {
        public ParsedCommand(string source)
        {
            var parts = source.Split(' ');
            this.Type = parts[0];
            this.Args = parts.Skip(1).ToArray();
        }

        public string Type { get; private set; }
        public string[] Args { get; private set; }

        public int Count
        {
            get { return this.Args.Length; }
        }

        public string GetTrail(int index)
        {
            return String.Join(" ", this.Args.Skip(index));
        }
    }
}
