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