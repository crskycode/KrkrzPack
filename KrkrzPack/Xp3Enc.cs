using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KrkrzPack
{
    static class Xp3Enc
    {
        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Ansi)]
        static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpFileName);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool FreeLibrary(IntPtr hModule);

        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        delegate void XP3ArchiveAttractFilter_v2(uint hash, long offset, byte[] buffer, int bufferlen);

        static IntPtr _hDll;
        static XP3ArchiveAttractFilter_v2 _XP3ArchiveAttractFilter_v2;

        public static bool Load(string fileName)
        {
            _hDll = LoadLibrary(fileName);
            if (_hDll == IntPtr.Zero)
                return false;
            IntPtr pfn = GetProcAddress(_hDll, "XP3ArchiveAttractFilter_v2");
            if (pfn == IntPtr.Zero)
                return false;
            _XP3ArchiveAttractFilter_v2 = Marshal.GetDelegateForFunctionPointer<XP3ArchiveAttractFilter_v2>(pfn);
            return _XP3ArchiveAttractFilter_v2 != null;
        }

        public static bool Loaded
        {
            get => _XP3ArchiveAttractFilter_v2 != null;
        }

        public static void Encrypt(uint hash, long offset, byte[] buffer)
        {
            if (_XP3ArchiveAttractFilter_v2 == null)
                return;
            _XP3ArchiveAttractFilter_v2(hash, offset, buffer, buffer.Length);
        }

        public static void Decrypt(uint hash, long offset, byte[] buffer)
        {
            if (_XP3ArchiveAttractFilter_v2 == null)
                return;
            _XP3ArchiveAttractFilter_v2(hash, offset, buffer, buffer.Length);
        }
    }
}
