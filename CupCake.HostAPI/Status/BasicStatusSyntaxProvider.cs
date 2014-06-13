using System;
using System.Linq;

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