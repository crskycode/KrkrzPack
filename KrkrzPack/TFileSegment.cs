using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrkrzPack
{
    class TFileSegment
    {
        public long Offset { get; set; }
        public long Size { get; set; }
        public long StoreSize { get; set; }
        public uint Flags { get; set; }
    }
}
