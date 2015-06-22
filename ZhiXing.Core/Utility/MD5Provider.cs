using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhiXing.Core.Utility
{
    public class MD5Provider
    {
        public static string GetMD5FromFile(Stream inputStream)
        {  
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();

            byte[] hashByte = md5.ComputeHash(inputStream);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hashByte.Length; i++)
            {
                sb.Append(hashByte[i].ToString("x2"));
            }

            return sb.ToString(); 
        }
    }
}
