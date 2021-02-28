using ICSharpCode.SharpZipLib.Checksum;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrkrzPack
{
    static class Util
    {
        public static string MakeDirectoryPath(string path)
        {
            string newPath = path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            if (!newPath.EndsWith(new string(Path.DirectorySeparatorChar, 1)))
                newPath += Path.DirectorySeparatorChar;
            return newPath;
        }

        public static string GetRelativePath(string pathFrom, string pathTo)
        {
            Uri uri = new Uri(pathFrom);
            string relativePath = Uri.UnescapeDataString(uri.MakeRelativeUri(new Uri(pathTo)).ToString());
            relativePath = relativePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            if (!relativePath.Contains(Path.DirectorySeparatorChar.ToString()))
                relativePath = "." + Path.DirectorySeparatorChar + relativePath;
            return relativePath;
        }

        public static uint Adler32(byte[] buffer)
        {
            Adler32 adler32 = new Adler32();
            adler32.Update(buffer);
            return (uint)adler32.Value;
        }

        public static byte[] Deflate(byte[] buffer)
        {
            Deflater deflater = new Deflater(Deflater.BEST_COMPRESSION);
            using (MemoryStream memoryStream = new MemoryStream())
            using (DeflaterOutputStream deflaterOutputStream = new DeflaterOutputStream(memoryStream, deflater))
            {
                deflaterOutputStream.Write(buffer, 0, buffer.Length);
                deflaterOutputStream.Flush();
                deflaterOutputStream.Finish();

                return memoryStream.ToArray();
            }
        }

        public static byte[] Inflate(byte[] buffer)
        {
            byte[] block = new byte[256];
            MemoryStream outputStream = new MemoryStream();

            Inflater inflater = new Inflater();
            using (MemoryStream memoryStream = new MemoryStream(buffer))
            using (InflaterInputStream inflaterInputStream = new InflaterInputStream(memoryStream, inflater))
            {
                while (true)
                {
                    int numBytes = inflaterInputStream.Read(block, 0, block.Length);
                    if (numBytes < 1)
                        break;
                    outputStream.Write(block, 0, numBytes);
                }
            }

            return outputStream.ToArray();
        }

        public static string StringMd5(string input)
        {
            System.Security.Cryptography.MD5 alg = System.Security.Cryptography.MD5.Create();
            byte[] hash = alg.ComputeHash(Encoding.Unicode.GetBytes(input));
            StringBuilder sb = new StringBuilder(32);
            for (int i = 0; i < hash.Length; i++)
                sb.Append(hash[i].ToString("x2"));
            return sb.ToString();
        }

        public static void ExtractIndexData(string fileName, string outputFile)
        {
            using (FileStream stream = File.OpenRead(fileName))
            using (BinaryReader reader = new BinaryReader(stream))
            {
                stream.Position = 0x20;
                stream.Position = reader.ReadInt64(); // Seek to index
                reader.ReadByte(); // 0x01
                int compr_size = (int)reader.ReadInt64();
                int index_size = (int)reader.ReadInt64();
                byte[] compr_data = reader.ReadBytes(compr_size);
                byte[] uncompr_data = Inflate(compr_data);
                File.WriteAllBytes(outputFile, uncompr_data);
            }
        }
    }
}
