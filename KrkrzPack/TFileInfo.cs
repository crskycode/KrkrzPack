using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrkrzPack
{
    class TFileInfo
    {
        public string Name { get; set; }
        public string NameMd5 { get; set; }
        public long Timestamp { get; set; }
        public long Size { get; set; }
        public long StoreSize { get; set; }
        public uint Adler32 { get; set; }
        public uint Flags { get; set; }
        public List<TFileSegment> Segments { get; set; } = new List<TFileSegment>();
    }
}
