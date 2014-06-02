using ProtoBuf;

namespace CupCake.Protocol
{
    [ProtoContract]
    public class Authentication
    {
        [ProtoMember(1)]
        public string Pin { get; set; }
    }
}