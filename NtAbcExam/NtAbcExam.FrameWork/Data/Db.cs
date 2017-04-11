using Dos.ORM;
using NtAbcExam.FrameWork.Logging;

namespace NtAbcExam.FrameWork.Data
{
    public class Db
    {
        public static readonly DbSession Context = new DbSession("SQLServerConnection");

        static Db()
        {
            Context.RegisterSqlLogger(delegate (string sql)
            {
                LogHelper.Debug("SQL日志:" + sql);
            });
        }
    }
}
