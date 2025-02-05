﻿using System.Diagnostics;
using MHTriServer.Utils;

namespace MHTriServer.Server.Packets
{
    public class ReqUserBinarySet : Packet
    {
        public const uint PACKET_ID = 0x66310100;

        public uint UnknownField { get; private set; }

        public byte[] UnknownField2 { get; private set; }

        public ReqUserBinarySet(uint unknownField, byte[] unknownField2) : base(PACKET_ID) => (UnknownField, UnknownField2) = (unknownField, unknownField2);

        public ReqUserBinarySet(uint id, ushort size, ushort counter) : base(id, size, counter) { }

        public override void Serialize(BEBinaryWriter writer)
        {
            base.Serialize(writer);
            writer.Write(UnknownField);
            writer.WriteShortBytes(UnknownField2);
        }

        public override void Deserialize(BEBinaryReader reader)
        {
            Debug.Assert(ID == PACKET_ID);
            UnknownField = reader.ReadUInt32();
            UnknownField2 = reader.ReadShortBytes();
        }

        public override void Handle(PacketHandler handler, NetworkSession networkSession) =>
            handler.HandleReqUserBinarySet(networkSession, this);

        public override string ToString()
        {
            return base.ToString() + $"\n\tUnknownField {UnknownField}\n\tUnknownField2 '{Packet.Hexstring(UnknownField2, ' ')}'";
        }
    }
}
