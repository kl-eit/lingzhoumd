using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Ling.Common
{
    public class Security
    {
        /// <summary>
        /// Simple Hash
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"> </param>
        /// <returns></returns>
        public static string Hash(string password, string salt = "")
        {
            return BitConverter.ToString(SHA1.Create().ComputeHash(Encoding.Default.GetBytes(password + salt))).Replace("-", "");
        }
    }
}
