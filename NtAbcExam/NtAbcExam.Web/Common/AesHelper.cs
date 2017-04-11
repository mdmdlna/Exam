using NtAbcExam.FrameWork.Encryption;

namespace NtAbcExam.Web.Common
{
    public static class AesHelper
    {
        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string Decrypt(string content)
        {
            return AES.Decrypt(content, GlobalVar.AesKey, GlobalVar.AesIv);
        }

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string Encrypt(string content)
        {
            return AES.Encrypt(content, GlobalVar.AesKey, GlobalVar.AesIv);
        }
    }
}