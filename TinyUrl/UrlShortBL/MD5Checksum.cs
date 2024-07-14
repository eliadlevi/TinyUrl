using System.Security.Cryptography;
using System.Text;

namespace TinyUrl.UrlShortBL
{
    // Using the Md5 algorithem to hash the data 
    public class MD5Checksum : IChecksum
    {
        public MD5Checksum() { }
        public string Run(string value)
        {
            return CreateMD5(value);
        }

        private static string CreateMD5(string input)
        {
            using (var md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                return Convert.ToHexString(hashBytes);
            }
        }

    }
}
