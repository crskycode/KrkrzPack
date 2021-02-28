using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrkrzPack
{
    class ChunkWriter
    {
        readonly BinaryWriter _writer;
        readonly string _magic;
        long _pos1;
        long _pos2;

        public ChunkWriter(BinaryWriter writer, string magic)
        {
            _writer = writer;
            _magic = magic;
        }

        void Begin(string magic)
        {
            _writer.Write(Encoding.ASCII.GetBytes(magic));
            _pos1 = _writer.BaseStream.Position;
            _writer.WriteInt64(0);
            _pos2 = _writer.BaseStream.Position;
        }

        public void End()
        {
            long pos = _writer.BaseStream.Position;
            long chunkSize = pos - _pos2;
            _writer.BaseStream.Position = _pos1;
            _writer.WriteInt64(chunkSize);
            _writer.BaseStream.Position = pos;
        }

        public void Write(Action<BinaryWriter> action)
        {
            Begin(_magic);
            action(_writer);
            End();
        }
    }
}
