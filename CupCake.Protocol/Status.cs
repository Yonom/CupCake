using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace CupCake.Protocol
{
    [ProtoContract]
    public class Status
    {
        [ProtoMember(1)]
        public string Text { get; set; }
    }
}
