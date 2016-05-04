using System;
using System.Security.Cryptography;
using System.Text;

namespace SimpleCrypto
{
    public static class md5
    {

        public static string GetMD5(string text)
        {
            MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
            MD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            byte[] res = MD5.Hash;
            StringBuilder sb = new StringBuilder();

            for (int cnt = 1; cnt < res.Length; ++cnt)
            {
                sb.Append(res[cnt].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}