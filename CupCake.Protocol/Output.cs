using ProtoBuf;

namespace CupCake.Protocol
{
    [ProtoContract]
    public class Output
    {
        [ProtoMember(1)]
        public string Text { get; set; }
    }
}