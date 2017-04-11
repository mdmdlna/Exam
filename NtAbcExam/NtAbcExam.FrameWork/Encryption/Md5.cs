using System;
using System.IO;
using System.Text;

namespace NtAbcExam.FrameWork.Encryption
{
    public static class Md5
    {
        //static state variables
        private static UInt32 _a;
        private static UInt32 _b;
        private static UInt32 _c;
        private static UInt32 _d;

        #region number of bits to rotate in tranforming
        private const int S11 = 7;
        private const int S12 = 12;
        private const int S13 = 17;
        private const int S14 = 22;
        private const int S21 = 5;
        private const int S22 = 9;
        private const int S23 = 14;
        private const int S24 = 20;
        private const int S31 = 4;
        private const int S32 = 11;
        private const int S33 = 16;
        private const int S34 = 23;
        private const int S41 = 6;
        private const int S42 = 10;
        private const int S43 = 15;
        private const int S44 = 21;
        #endregion

        #region F, G, H and I are basic MD5 functions
        /* F, G, H and I are basic MD5 functions.
        * 四个非线性函数:
        *
        * F(X,Y,Z) =(X&Y)|((~X)&Z)
        * G(X,Y,Z) =(X&Z)|(Y&(~Z))
        * H(X,Y,Z) =X^Y^Z
        * I(X,Y,Z)=Y^(X|(~Z))
        *
        * （&与，|或，~非，^异或）
        */
        private static UInt32 F(UInt32 x, UInt32 y, UInt32 z)
        {
            return (x & y) | ((~x) & z);
        }
        private static UInt32 G(UInt32 x, UInt32 y, UInt32 z)
        {
            return (x & z) | (y & (~z));
        }
        private static UInt32 H(UInt32 x, UInt32 y, UInt32 z)
        {
            return x ^ y ^ z;
        }
        private static UInt32 I(UInt32 x, UInt32 y, UInt32 z)
        {
            return y ^ (x | (~z));
        }
        #endregion

        #region FF, GG, HH, and II transformations for rounds 1, 2, 3, and 4
        /* FF, GG, HH, and II transformations for rounds 1, 2, 3, and 4.
        * Rotation is separate from addition to prevent recomputation.
        */
        private static void FF(ref UInt32 a, UInt32 b, UInt32 c, UInt32 d, UInt32 mj, int s, UInt32 ti)
        {
            a = a + F(b, c, d) + mj + ti;
            a = a << s | a >> (32 - s);
            a += b;
            // 转换为 ：a=b+((a+F(b,c,d)+mj+ti)<<s)
        }

        private static void GG(ref UInt32 a, UInt32 b, UInt32 c, UInt32 d, UInt32 mj, int s, UInt32 ti)
        {
            a = a + G(b, c, d) + mj + ti;
            a = a << s | a >> (32 - s);
            a += b;
        }
        private static void HH(ref UInt32 a, UInt32 b, UInt32 c, UInt32 d, UInt32 mj, int s, UInt32 ti)
        {
            a = a + H(b, c, d) + mj + ti;
            a = a << s | a >> (32 - s);
            a += b;
        }
        private static void II(ref UInt32 a, UInt32 b, UInt32 c, UInt32 d, UInt32 mj, int s, UInt32 ti)
        {
            a = a + I(b, c, d) + mj + ti;
            a = a << s | a >> (32 - s);
            a += b;
        }
        #endregion

        private static void MD5_Init()
        {
            _a = 0x67452301; //in memory, this is 0x01234567
            _b = 0xefcdab89; //in memory, this is 0x89abcdef
            _c = 0x98badcfe; //in memory, this is 0xfedcba98
            _d = 0x10325476; //in memory, this is 0x76543210
        }

        private static void MD5_TrasformBlock(byte[] block)
        {
            UInt32[] x = new UInt32[16];
            for (int i = 0, j = 0; i < 64; i += 4, j++)
                x[j] = (UInt32)(block[i] | block[i + 1] << 8 | block[i + 2] << 16 | block[i + 3] << 24);
            uint a = _a;
            uint b = _b;
            uint c = _c;
            uint d = _d;
            /* Round 1 */
            FF(ref a, b, c, d, x[0], S11, 0xd76aa478); /* 1 */
            FF(ref d, a, b, c, x[1], S12, 0xe8c7b756); /* 2 */
            FF(ref c, d, a, b, x[2], S13, 0x242070db); /* 3 */
            FF(ref b, c, d, a, x[3], S14, 0xc1bdceee); /* 4 */
            FF(ref a, b, c, d, x[4], S11, 0xf57c0faf); /* 5 */
            FF(ref d, a, b, c, x[5], S12, 0x4787c62a); /* 6 */
            FF(ref c, d, a, b, x[6], S13, 0xa8304613); /* 7 */
            FF(ref b, c, d, a, x[7], S14, 0xfd469501); /* 8 */
            FF(ref a, b, c, d, x[8], S11, 0x698098d8); /* 9 */
            FF(ref d, a, b, c, x[9], S12, 0x8b44f7af); /* 10 */
            FF(ref c, d, a, b, x[10], S13, 0xffff5bb1); /* 11 */
            FF(ref b, c, d, a, x[11], S14, 0x895cd7be); /* 12 */
            FF(ref a, b, c, d, x[12], S11, 0x6b901122); /* 13 */
            FF(ref d, a, b, c, x[13], S12, 0xfd987193); /* 14 */
            FF(ref c, d, a, b, x[14], S13, 0xa679438e); /* 15 */
            FF(ref b, c, d, a, x[15], S14, 0x49b40821); /* 16 */
            /* Round 2 */
            GG(ref a, b, c, d, x[1], S21, 0xf61e2562); /* 17 */
            GG(ref d, a, b, c, x[6], S22, 0xc040b340); /* 18 */
            GG(ref c, d, a, b, x[11], S23, 0x265e5a51); /* 19 */
            GG(ref b, c, d, a, x[0], S24, 0xe9b6c7aa); /* 20 */
            GG(ref a, b, c, d, x[5], S21, 0xd62f105d); /* 21 */
            GG(ref d, a, b, c, x[10], S22, 0x2441453); /* 22 */
            GG(ref c, d, a, b, x[15], S23, 0xd8a1e681); /* 23 */
            GG(ref b, c, d, a, x[4], S24, 0xe7d3fbc8); /* 24 */
            GG(ref a, b, c, d, x[9], S21, 0x21e1cde6); /* 25 */
            GG(ref d, a, b, c, x[14], S22, 0xc33707d6); /* 26 */
            GG(ref c, d, a, b, x[3], S23, 0xf4d50d87); /* 27 */
            GG(ref b, c, d, a, x[8], S24, 0x455a14ed); /* 28 */
            GG(ref a, b, c, d, x[13], S21, 0xa9e3e905); /* 29 */
            GG(ref d, a, b, c, x[2], S22, 0xfcefa3f8); /* 30 */
            GG(ref c, d, a, b, x[7], S23, 0x676f02d9); /* 31 */
            GG(ref b, c, d, a, x[12], S24, 0x8d2a4c8a); /* 32 */
            /* Round 3 */
            HH(ref a, b, c, d, x[5], S31, 0xfffa3942); /* 33 */
            HH(ref d, a, b, c, x[8], S32, 0x8771f681); /* 34 */
            HH(ref c, d, a, b, x[11], S33, 0x6d9d6122); /* 35 */
            HH(ref b, c, d, a, x[14], S34, 0xfde5380c); /* 36 */
            HH(ref a, b, c, d, x[1], S31, 0xa4beea44); /* 37 */
            HH(ref d, a, b, c, x[4], S32, 0x4bdecfa9); /* 38 */
            HH(ref c, d, a, b, x[7], S33, 0xf6bb4b60); /* 39 */
            HH(ref b, c, d, a, x[10], S34, 0xbebfbc70); /* 40 */
            HH(ref a, b, c, d, x[13], S31, 0x289b7ec6); /* 41 */
            HH(ref d, a, b, c, x[0], S32, 0xeaa127fa); /* 42 */
            HH(ref c, d, a, b, x[3], S33, 0xd4ef3085); /* 43 */
            HH(ref b, c, d, a, x[6], S34, 0x4881d05); /* 44 */
            HH(ref a, b, c, d, x[9], S31, 0xd9d4d039); /* 45 */
            HH(ref d, a, b, c, x[12], S32, 0xe6db99e5); /* 46 */
            HH(ref c, d, a, b, x[15], S33, 0x1fa27cf8); /* 47 */
            HH(ref b, c, d, a, x[2], S34, 0xc4ac5665); /* 48 */
            /* Round 4 */
            II(ref a, b, c, d, x[0], S41, 0xf4292244); /* 49 */
            II(ref d, a, b, c, x[7], S42, 0x432aff97); /* 50 */
            II(ref c, d, a, b, x[14], S43, 0xab9423a7); /* 51 */
            II(ref b, c, d, a, x[5], S44, 0xfc93a039); /* 52 */
            II(ref a, b, c, d, x[12], S41, 0x655b59c3); /* 53 */
            II(ref d, a, b, c, x[3], S42, 0x8f0ccc92); /* 54 */
            II(ref c, d, a, b, x[10], S43, 0xffeff47d); /* 55 */
            II(ref b, c, d, a, x[1], S44, 0x85845dd1); /* 56 */
            II(ref a, b, c, d, x[8], S41, 0x6fa87e4f); /* 57 */
            II(ref d, a, b, c, x[15], S42, 0xfe2ce6e0); /* 58 */
            II(ref c, d, a, b, x[6], S43, 0xa3014314); /* 59 */
            II(ref b, c, d, a, x[13], S44, 0x4e0811a1); /* 60 */
            II(ref a, b, c, d, x[4], S41, 0xf7537e82); /* 61 */
            II(ref d, a, b, c, x[11], S42, 0xbd3af235); /* 62 */
            II(ref c, d, a, b, x[2], S43, 0x2ad7d2bb); /* 63 */
            II(ref b, c, d, a, x[9], S44, 0xeb86d391); /* 64 */
            _a += a;
            _b += b;
            _c += c;
            _d += d;
        }

        /// <summary>
        /// 得到stream的md5
        /// </summary>
        /// <param name="stream">要处理的流</param>
        /// <param name="index">开始处理的位置</param>
        /// <param name="count">要处理的长度</param>
        /// <returns></returns>
        public static string Compute(Stream stream, long index, long count)
        {
            if (stream == null)
                throw new ArgumentNullException();
            byte[] block = new byte[64];
            UInt64 size = 0;
            int b;
            MD5_Init();

            while (index-- > 0 && stream.ReadByte() >= 0)
            {
            }

            if (index >= 0)
            {
                throw new ArgumentException("", "index");
            }

            while ((b = stream.ReadByte()) != -1 && count-- > 0)
            {
                block[size++ % 64] = (byte)b;
                if (size % 64 == 0)
                {
                    MD5_TrasformBlock(block);
                }
            }
            if (count > 0)
            {
                throw new ArgumentException("", "count");
            }
            UInt64 bp = size % 64;//blockPoint
            block[bp++] = 0x80;
            if (bp > 56)
            {
                while (bp < 64)
                {
                    block[bp++] = 0;
                }
                MD5_TrasformBlock(block);
                for (int i = 0; i < 56; i++)
                {
                    block[i] = 0;
                }
            }
            else
            {
                while (bp < 56)
                {
                    block[bp++] = 0;
                }
            }
            size *= 8;
            block[56] = (byte)(size & 0xFF);
            block[57] = (byte)((size >> 8) & 0xFF);
            block[58] = (byte)((size >> 16) & 0xFF);
            block[59] = (byte)((size >> 24) & 0xFF);
            block[60] = (byte)((size >> 32) & 0xFF);
            block[61] = (byte)((size >> 40) & 0xFF);
            block[62] = (byte)((size >> 48) & 0xFF);
            block[63] = (byte)(size >> 56);
            MD5_TrasformBlock(block);
            //return new UInt32[] { A, B, C, D };
            return string.Format("{0}-{1}-{2}-{3}", BitConverter.ToString(BitConverter.GetBytes(_a))
                , BitConverter.ToString(BitConverter.GetBytes(_b))
                , BitConverter.ToString(BitConverter.GetBytes(_c))
                , BitConverter.ToString(BitConverter.GetBytes(_d))).Replace("-", "").ToLower();
        }

        public static string Compute(string str)
        {
            Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(str));
            return Compute(stream, 0, stream.Length).ToLower();
        }
    }
}
