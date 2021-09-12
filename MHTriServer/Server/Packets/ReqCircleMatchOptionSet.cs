﻿using System.Diagnostics;

namespace MHTriServer.Server.Packets
{
    public class ReqCircleMatchOptionSet : Packet
    {
        public const uint PACKET_ID = 0x65100100;

        public CompoundList MatchOptions { get; private set; }

        public ReqCircleMatchOptionSet(CompoundList matchOptions) : base(PACKET_ID) => (MatchOptions) = (matchOptions);

        public ReqCircleMatchOptionSet(uint id, ushort size, ushort counter) : base(id, size, counter) { }

        public override void Serialize(ExtendedBinaryWriter writer)
        {
            base.Serialize(writer);
            MatchOptions.Serialize(writer);
        }

        public override void Deserialize(ExtendedBinaryReader reader)
        {
            Debug.Assert(ID == PACKET_ID);
            MatchOptions = CompoundList.Deserialize<CompoundList>(reader);
        }

        public override string ToString()
        {
            return base.ToString() + $":\n\tMatchOptions\n{MatchOptions}";
        }
    }
}
