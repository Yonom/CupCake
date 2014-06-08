using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CupCake.HostAPI.Status
{
    public class BasicStatusSyntaxProvider : IStatusSyntaxProvider
    {
        public string Parse(StatusItem[] statuses)
        {
            return String.Join(", ", statuses.Select(s => String.Format("{0} = {1}", s.Name, s.Value)));
        }
    }
}
