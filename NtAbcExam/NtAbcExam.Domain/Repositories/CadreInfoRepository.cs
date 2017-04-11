
using System.Collections.Generic;
using NtAbcExam.Domain.Models;
using NtAbcExam.FrameWork.Abstractions;
using NtAbcExam.FrameWork.Data;

namespace NtAbcExam.Domain.Repositories
{
    public class CadreInfoRepository : Repository<cadre_info>
    {
        public cadre_info GetModelById(int id)
        {

            return Db.Context.From<cadre_info>().Where(q => q.Id == id).First();
        }

        public cadre_info GetModelByUserId(string userId)
        {
            return Db.Context.From<cadre_info>().Where(q => q.UserID == userId).First();
        }

        public cadre_info Login(string userId, string userPwd)
        {
            return Db.Context.From<cadre_info>().Where(q => q.UserID == userId && q.Pwd == userPwd).ToFirst();
        }

        public bool ChangePwd(string userId, string pwd)
        {
            return Db.Context.Update<cadre_info>(cadre_info._.Pwd, pwd, cadre_info._.Id == userId) > 0;
        }


        public List<cadre_info> GetListByDeptId(int deptId)
        {
            return Db.Context.From<cadre_info>().Where(q => q.DeptId == deptId).ToList();
        }
    }
}
