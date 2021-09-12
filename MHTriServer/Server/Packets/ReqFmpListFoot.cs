﻿using System.Diagnostics;

namespace MHTriServer.Server.Packets
{
    public class ReqFmpListFoot : Packet
    {
        public const uint PACKET_ID = 0x61330100;
        public const uint PACKET_ID_FMP = 0x63130100;

        public ReqFmpListFoot() : base(PACKET_ID) { }

        public ReqFmpListFoot(bool isServerFmp) : base(PACKET_ID_FMP) { }

        public ReqFmpListFoot(uint id, ushort size, ushort counter) : base(id, size, counter) { }

        public override void Serialize(ExtendedBinaryWriter writer)
        {
            base.Serialize(writer);
        }

        public override void Deserialize(ExtendedBinaryReader reader)
        {
            Debug.Assert(ID == PACKET_ID || ID == PACKET_ID_FMP);
            Debug.Assert(Size == 0);
        }
    }
}
