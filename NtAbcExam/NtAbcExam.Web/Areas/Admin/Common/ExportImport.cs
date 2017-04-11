using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using NtAbcExam.FrameWork.Logging;

namespace NtAbcExam.Web.Areas.Admin.Common
{
    public class ExportImport
    {
        /// <summary>
        /// 返回dataTable
        /// </summary>
        /// <param name="savePath"></param>
        /// <param name="isDelFile"></param>
        /// <returns></returns>
        public DataTable GetFillData(string savePath, bool isDelFile)
        {
            try
            {
                string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + savePath + ";" + "Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'";
                //只读第一个表
                OleDbConnection oledbconn1 = new OleDbConnection(strConn);
                oledbconn1.Open();
                DataTable table = oledbconn1.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                if (table.Rows.Count > 0)
                {
                    var strTableName = table.Rows[0]["TABLE_NAME"].ToString().Trim();
                    string sql = string.Format("SELECT * FROM [{0}]", strTableName);
                    table = new DataTable();
                    OleDbDataAdapter da = new OleDbDataAdapter(sql, oledbconn1);
                    da.Fill(table);
                }
                oledbconn1.Close();
                if (isDelFile)
                {
                    string path = savePath.Substring(0, savePath.LastIndexOf('/') + 1);
                    Directory.Delete(path, true);
                }
                return table;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 检查整行是否为空
        /// </summary>
        /// <param name="dr"></param>
        /// <returns>bool-true通过false不通过</returns>
        public bool ChkAllIsEmpty(DataRow dr)
        {

            for (int n = 0; n < dr.ItemArray.Length; n++)
            {
                if (!string.Empty.Equals(dr.ItemArray[n].ToString().Trim()))
                {
                    return false;
                }
            }
            return true;
        }

    }
}