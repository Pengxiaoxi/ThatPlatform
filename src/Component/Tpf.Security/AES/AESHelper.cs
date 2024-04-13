using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Encoders;
using System.Text;

namespace Tpf.Security
{
    /// <summary>
    /// AES Sercutiry Helper
    /// </summary>
    public class AESHelper
    {
        /// <summary>
        /// AES 加密方法
        /// </summary>
        /// <param name="plaintext">原文</param>
        /// <param name="key">密钥</param>
        /// <param name="iv">偏移量</param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] ciphertext, byte[] key, byte[] iv)
        {
            IBufferedCipher cipher = CipherUtilities.GetCipher("AES/CTR/PKCS7Padding");
            cipher.Init(true, new ParametersWithIV(ParameterUtilities.CreateKeyParameter("AES", key), iv));
            return cipher.DoFinal(ciphertext);
        }

        /// <summary>
        /// AES 解密方法
        /// </summary>
        /// <param name="ciphertext">密文</param>
        /// <param name="key">密钥</param>
        /// <param name="iv">偏移量</param>
        /// <returns></returns>
        public static byte[] Decrypt(byte[] ciphertext, byte[] key, byte[] iv)
        {
            IBufferedCipher cipher = CipherUtilities.GetCipher("AES/CTR/PKCS7Padding");
            cipher.Init(false, new ParametersWithIV(ParameterUtilities.CreateKeyParameter("AES", key), iv));
            byte[] plaintext = cipher.DoFinal(ciphertext);
            return plaintext;
        }

        /// <summary>
        /// AES 加密
        /// </summary>
        /// <param name="content">原文</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static string Encrypt(string content, string key)
        {
            var cipherStr = Base64.ToBase64String(Encrypt(Encoding.UTF8.GetBytes(content), Encoding.UTF8.GetBytes(key), new byte[16]));

            return cipherStr;
        }

        /// <summary>
        /// AES 解密
        /// </summary>
        /// <param name="content">密文</param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Decrypt(string content, string key)
        {
            var originalStr = Encoding.UTF8.GetString(Decrypt(Base64.Decode(content), Encoding.UTF8.GetBytes(key), new byte[16]));

            return originalStr;
        }

    }
}
