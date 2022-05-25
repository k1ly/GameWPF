using System;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace GameWPF.Util.Hash
{
    public class SaltedHash
    {
        public byte[] Hash { get; set; }
        public byte[] Salt { get; set; }

        public SaltedHash(string psw)
        {
            byte[] saltBytes = new byte[32];
            new Random().NextBytes(saltBytes);
            Salt = Encoding.UTF8.GetBytes(Convert.ToBase64String(saltBytes));
            byte[] passwordAndSaltBytes = Concat(psw, saltBytes);
            Hash = ComputeHash(passwordAndSaltBytes);
        }

        public static bool Verify(byte[] hash, byte[] salt, string psw)
        {
            byte[] saltBytes = Convert.FromBase64String(Encoding.UTF8.GetString(salt));
            var passwordAndSaltBytes = Concat(psw, saltBytes);
            var hashAttempt = ComputeHash(passwordAndSaltBytes);
            return Enumerable.SequenceEqual(hash, hashAttempt);
        }

        private static byte[] ComputeHash(byte[] passwordAndSaltBytes)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                return Encoding.UTF8.GetBytes(Convert.ToBase64String(sha256.ComputeHash(passwordAndSaltBytes)));
            }
        }

        private static byte[] Concat(string password, byte[] saltBytes)
            => Encoding.UTF8.GetBytes(password).Concat(saltBytes).ToArray();
    }
}
