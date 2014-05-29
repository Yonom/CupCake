using ProtoBuf;

namespace CupCake.Protocol
{

    [ProtoContract]
    public class Input
    {
        [ProtoMember(1)]
        public string Text { get; set; }
    }
}
