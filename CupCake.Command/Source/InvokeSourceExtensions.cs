using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CupCake.Command.Source
{
    public static class InvokeSourceExtensions
    {
        public static PlayerInvokeSource ToPlayerInvokeSource(this IInvokeSource source)
        {
            var playerSource = source as PlayerInvokeSource;
            if (playerSource == null)
                throw new InvalidInvokeSourceCommandException("You must call this command as a player.");

            return playerSource;
        }
    }
}
