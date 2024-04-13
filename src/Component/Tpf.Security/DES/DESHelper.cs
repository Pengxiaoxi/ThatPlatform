using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace Tpf.Security.DES
{
    /// <summary>
    /// DES Sercutiry Helper
    /// </summary>
    public class DESHelper
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data">待加密原文数据</param>
        /// <param name="key">密钥</param>
        /// <param name="iv">偏移量，ECB模式不用填写！</param>
        /// <param name="algorithm">密文算法</param>
        /// <returns>密文数据</returns>
        public static byte[] Encrypt(byte[] data, byte[] key, byte[] iv, string algorithm)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (algorithm == null)
            {
                throw new ArgumentNullException(nameof(algorithm));
            }

            var cipher = CipherUtilities.GetCipher(algorithm);
            if (iv == null)
            {
                cipher.Init(true, ParameterUtilities.CreateKeyParameter("DES", key));
            }
            else
            {
                cipher.Init(true, new ParametersWithIV(ParameterUtilities.CreateKeyParameter("DES", key), iv));
            }

            return cipher.DoFinal(data);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data">待解密数据</param>
        /// <param name="key">密钥</param>
        /// <param name="iv">偏移量，ECB模式不用填写！</param>
        /// <param name="algorithm">密文算法</param>
        /// <returns>未加密原文数据</returns>
        public static byte[] Decrypt(byte[] data, byte[] key, byte[] iv, string algorithm)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var cipher = CipherUtilities.GetCipher(algorithm);
            if (iv == null)
            {
                cipher.Init(false, ParameterUtilities.CreateKeyParameter("DES", key));
            }
            else
            {
                cipher.Init(false, new ParametersWithIV(ParameterUtilities.CreateKeyParameter("DES", key), iv));
            }
            return cipher.DoFinal(data);
        }

    }
}
