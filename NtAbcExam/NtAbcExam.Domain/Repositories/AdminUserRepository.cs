using NtAbcExam.Domain.DataModels;
using NtAbcExam.Domain.Models;
using NtAbcExam.FrameWork.Abstractions;
using NtAbcExam.FrameWork.Data;

namespace NtAbcExam.Domain.Repositories
{
    public class AdminUserRepository : Repository<AdminUser>
    {
        public AdminUser GetModelById(int id)
        {
            return Db.Context.From<AdminUser>().Where(q => q.Id == id).First();
        }
       

        public AdminUser Login(string userName, string userPwd)
        {
            return Db.Context.From<AdminUser>().Where(q => q.UserName  == userName && q.UserPwd == userPwd).ToFirst();
        }


        public bool ChangePwd(int id, string pwd)
        {
            return Db.Context.Update<AdminUser>(AdminUser._.UserPwd, pwd, AdminUser._.Id == id) > 0;
        }
    }
}
