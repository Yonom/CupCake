using ProtoBuf;

namespace CupCake.Protocol
{
    [ProtoContract]
    public class Title
    {
        [ProtoMember(1)]
        public string Text { get; set; }
    }
}
