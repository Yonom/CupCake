using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CupCake.HostAPI.Status;

namespace CupCake.Server.SyntaxProviders
{
    public class CupCakeStatusSyntaxProvider : IStatusSyntaxProvider
    {
        public string Parse(StatusItem[] statuses)
        {
            return String.Join(" | ", statuses.Select(s => String.Format("{0}: {1}", s.Name, s.Value)));
        }
    }
}
