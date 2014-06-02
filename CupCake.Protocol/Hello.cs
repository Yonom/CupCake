using ProtoBuf;

namespace CupCake.Protocol
{
    [ProtoContract]
    public class Hello
    {
        public const int VersionNumber = 1;

        [ProtoMember(1)]
        public int Version { get; set; }
    }
}