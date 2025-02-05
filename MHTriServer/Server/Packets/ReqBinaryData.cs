﻿using System.Diagnostics;

using MHTriServer.Utils;

namespace MHTriServer.Server.Packets
{
    public class ReqBinaryData : Packet
    {
        public const uint PACKET_ID = 0x63030100;

        public byte Type { get; private set; }

        public uint Version { get; private set; }

        public uint Offset { get; private set; }

        public uint DataExpectedSize { get; private set; }

        public ReqBinaryData(byte binaryType, uint binaryVersion, uint binaryOffset, uint binaryExpectedSize) : base(PACKET_ID) 
            => (Type, Version, Offset, DataExpectedSize) = (binaryType, binaryVersion, binaryOffset, binaryExpectedSize);

        public ReqBinaryData(uint id, ushort size, ushort counter) : base(id, size, counter) { }

        public override void Serialize(BEBinaryWriter writer)
        {
            base.Serialize(writer);
            writer.Write(Type);
            writer.Write(Version);
            writer.Write(Offset);
            writer.Write(DataExpectedSize);
        }

        public override void Deserialize(BEBinaryReader reader)
        {
            Debug.Assert(ID == PACKET_ID);
            Debug.Assert(Size == 13);
            Type = reader.ReadByte();
            Version = reader.ReadUInt32();
            Offset = reader.ReadUInt32();
            DataExpectedSize = reader.ReadUInt32();
        }

        public override void Handle(PacketHandler handler, NetworkSession networkSession) =>
            handler.HandleReqBinaryData(networkSession, this);

        public override string ToString()
        {
            return base.ToString() + $"\n\tType {Type}\n\tVersion {Version}\n\tOffset {Offset}" +
                $"\n\tDataExpectedSize {DataExpectedSize}";
        }
    }
}
