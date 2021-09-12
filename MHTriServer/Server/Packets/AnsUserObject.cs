﻿using System.Diagnostics;

namespace MHTriServer.Server.Packets
{
    public class AnsUserObject : Packet
    {
        public const uint PACKET_ID = 0x61200200;

        public byte LoginInfoByte { get; private set; }

        public string UnknownString { get; private set; }

        public UserSlot UserObject { get; private set; }

        public AnsUserObject(byte loginInfoByte, string unknownString, UserSlot userObject) : base(PACKET_ID) 
            => (LoginInfoByte, UnknownString, UserObject) = (loginInfoByte, unknownString, userObject);

        public AnsUserObject(uint id, ushort size, ushort counter) : base(id, size, counter) { }

        public override void Serialize(ExtendedBinaryWriter writer)
        {
            base.Serialize(writer);
            writer.Write(LoginInfoByte);
            writer.Write(UnknownString);
            writer.Write((uint) 0); // This field is read, but is not used
            UserObject.Serialize(writer);
        }

        public override void Deserialize(ExtendedBinaryReader reader)
        {
            Debug.Assert(ID == PACKET_ID);
            LoginInfoByte = reader.ReadByte();
            UnknownString = reader.ReadString();
            _ = reader.ReadUInt32();
            UserObject = CompoundList.Deserialize<UserSlot>(reader);
        }
    }
}
