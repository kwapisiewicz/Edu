using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Exams.UI
{
    public class Md5PasswordHash : IHashFunction
    {
        private static readonly MD5 _hashFunction = MD5.Create();

        private static string GetMd5Hash(string input)
        {
            byte[] data = _hashFunction.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        public string Evaluate(string secureString)
        {
            return GetMd5Hash(secureString);
        }
    }
}
