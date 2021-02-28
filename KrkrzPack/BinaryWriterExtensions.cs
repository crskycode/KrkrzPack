using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrkrzPack
{
    static class BinaryWriterExtensions
    {
        public static void WriteBytes(this BinaryWriter writer, byte[] bytes)
        {
            writer.Write(bytes);
        }

        public static void WriteUnicodeString(this BinaryWriter writer, string @string)
        {
            writer.Write(Convert.ToUInt16(@string.Length));
            writer.Write(Encoding.Unicode.GetBytes(@string));
            writer.WriteUInt16(0);
        }

        public static void WriteByte(this BinaryWriter writer, byte value)
        {
            writer.Write(value);
        }

        public static void WriteInt16(this BinaryWriter writer, short value)
        {
            writer.Write(value);
        }

        public static void WriteUInt16(this BinaryWriter writer, ushort value)
        {
            writer.Write(value);
        }

        public static void WriteInt32(this BinaryWriter writer, int value)
        {
            writer.Write(value);
        }

        public static void WriteUInt32(this BinaryWriter writer, uint value)
        {
            writer.Write(value);
        }

        public static void WriteInt64(this BinaryWriter writer, long value)
        {
            writer.Write(value);
        }

        public static void WriteUInt64(this BinaryWriter writer, ulong value)
        {
            writer.Write(value);
        }
    }
}
