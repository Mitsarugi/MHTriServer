﻿using System.Diagnostics;

namespace MHTriServer.Server.Packets
{
    public class ReqMaintenance : Packet
    {
        public const uint PACKET_ID = 0x62200100;

        public ReqMaintenance() : base(PACKET_ID) { }

        public ReqMaintenance(uint id, ushort size, ushort counter) : base(id, size, counter) { }

        public override void Serialize(ExtendedBinaryWriter writer)
        {
            base.Serialize(writer);
        }

        public override void Deserialize(ExtendedBinaryReader reader)
        {
            Debug.Assert(ID == PACKET_ID);
            Debug.Assert(Size == 0);
        }
    }
}
