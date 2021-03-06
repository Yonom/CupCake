﻿using ProtoBuf;

namespace CupCake.Protocol
{
    [ProtoContract]
    public class SetData
    {
        [ProtoMember(1)]
        public string Email { get; set; }

        [ProtoMember(2)]
        public string Password { get; set; }

        [ProtoMember(3)]
        public string World { get; set; }

        [ProtoMember(4)]
        public string[] Directories { get; set; }

        [ProtoMember(6)]
        public DatabaseType DatabaseType { get; set; }

        [ProtoMember(7)]
        public string ConnectionString { get; set; }

        [ProtoMember(8)]
        public string Settings { get; set; }
    }
}