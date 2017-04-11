using System.Configuration;

namespace NtAbcExam.Web.Common
{
    public class GlobalVar
    {
        //不要修改此处的值
        public static readonly byte[] AesKey = { 88, 222, 206, 148, 169, 111, 243, 222, 153, 226, 117, 53, 222, 40, 133, 163, 217, 123, 57, 66, 55, 39, 174, 44, 180, 230, 151, 182, 100, 244, 75, 105 };
        //不要修改此处的值
        public static readonly byte[] AesIv = { 33, 232, 123, 122, 124, 126, 199, 111, 49, 12, 23, 148, 55, 112, 22, 62 };

        public static readonly int PageSize = ConfigurationManager.AppSettings["PageSiz"] == null ? 20 : int.Parse(ConfigurationManager.AppSettings["PageSiz"].ToString());
    }
}