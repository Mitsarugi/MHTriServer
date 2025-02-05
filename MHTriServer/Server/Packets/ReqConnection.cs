﻿using System.Diagnostics;

using MHTriServer.Utils;

namespace MHTriServer.Server.Packets
{
    public class ReqConnection : Packet
    {
        public const uint PACKET_ID = 0x60200100;

        public uint UnknownField { get; private set; }

        public ReqConnection(uint unknownField) : base(PACKET_ID) => UnknownField = unknownField;

        public ReqConnection(uint id, ushort size, ushort counter) : base(id, size, counter) { }

        public override void Serialize(BEBinaryWriter writer)
        {
            base.Serialize(writer);
            writer.Write(UnknownField);
        }

        public override void Deserialize(BEBinaryReader reader)
        {
            Debug.Assert(ID == PACKET_ID);
            Debug.Assert(Size == 4);

            UnknownField = reader.ReadUInt32();
        }
    }
}
