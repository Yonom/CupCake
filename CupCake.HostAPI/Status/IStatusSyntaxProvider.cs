using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CupCake.HostAPI.Status
{
    public interface IStatusSyntaxProvider
    {
        string Parse(StatusItem[] statuses);
    }
}
