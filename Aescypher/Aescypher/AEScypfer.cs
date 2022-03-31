using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Aescypher
{
    internal class AEScypfer
    {
        AesCryptoServiceProvider crypt_provider;
        public byte[] key;
        public byte[] IV;
        public AEScypfer()
        {
            crypt_provider = new AesCryptoServiceProvider();
            crypt_provider.BlockSize = 128;
            crypt_provider.KeySize = 128;
            crypt_provider.GenerateKey();
            crypt_provider.GenerateIV();
            crypt_provider.Padding = PaddingMode.Zeros;
            IV = crypt_provider.IV;
            key = crypt_provider.Key;
        }

        public string cypferECB(string text)
        {
            crypt_provider.Mode = CipherMode.ECB;
            crypt_provider.Key = key;
            ICryptoTransform transform = crypt_provider.CreateEncryptor();
            byte[] encrypted = transform.TransformFinalBlock(ASCIIEncoding.ASCII.GetBytes(text), 0, text.Length);
            return Convert.ToBase64String(encrypted);
        }

        public string decypferECB(string text)
        {
            crypt_provider.Mode = CipherMode.ECB;
            crypt_provider.Key = key;
            ICryptoTransform decrypt = crypt_provider.CreateDecryptor();
            byte[] encrypted = Convert.FromBase64String(text);
            byte[] decrypted = decrypt.TransformFinalBlock(encrypted, 0, encrypted.Length);
            return ASCIIEncoding.ASCII.GetString(decrypted);
        }

        public string cypferCBC(string text)
        {
            crypt_provider.Mode = CipherMode.CBC;
            crypt_provider.Key = key;
            crypt_provider.IV = IV;
            ICryptoTransform transform = crypt_provider.CreateEncryptor();
            byte[] encrypted = transform.TransformFinalBlock(ASCIIEncoding.ASCII.GetBytes(text), 0, text.Length);
            return Convert.ToBase64String(encrypted);
        }

        public string decypferCBC(string text)
        {
            crypt_provider.Mode = CipherMode.CBC;
            crypt_provider.Key = key;
            crypt_provider.IV = IV;
            ICryptoTransform transform = crypt_provider.CreateDecryptor();
            byte[] encrypted = Convert.FromBase64String(text);
            byte[] decrypted = transform.TransformFinalBlock(encrypted, 0, encrypted.Length);
            return ASCIIEncoding.ASCII.GetString(decrypted);
        }

        internal void randomKey()
        {
            crypt_provider.GenerateKey();
            key = crypt_provider.Key;
        }

        internal void randomIV()
        {
            crypt_provider.GenerateIV();
            IV = crypt_provider.IV;
        }
    }
}
