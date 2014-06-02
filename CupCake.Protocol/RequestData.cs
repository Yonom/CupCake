using ProtoBuf;

namespace CupCake.Protocol
{
    [ProtoContract]
    public class RequestData
    {
        [ProtoMember(1)]
        public bool IsDebug { get; set; }
    }
}