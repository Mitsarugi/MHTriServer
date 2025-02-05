﻿using System.Diagnostics;
using MHTriServer.Utils;

namespace MHTriServer.Server.Packets
{
    public class AnsUserBinaryNotice : Packet
    {
        public const uint PACKET_ID = 0x66320200;

        public AnsUserBinaryNotice() : base(PACKET_ID) { }

        public AnsUserBinaryNotice(uint id, ushort size, ushort counter) : base(id, size, counter) { }

        public override void Serialize(BEBinaryWriter writer)
        {
            base.Serialize(writer);
        }

        public override void Deserialize(BEBinaryReader reader)
        {
            Debug.Assert(ID == PACKET_ID);
            Debug.Assert(Size == 0);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
