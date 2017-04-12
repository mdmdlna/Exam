using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NtAbcExam.Domain.Models;
using NtAbcExam.FrameWork.Abstractions;
using NtAbcExam.FrameWork.Data;

namespace NtAbcExam.Domain.Repositories
{
    public class ExamTestUserRepository : Repository<exam_testuser>
    {
        public List<exam_testuser> GetMyTest(string userid)
        {
            return Db.Context.From<exam_testuser>().Where(q => q.UserId == userid && q.HaveTest == 0).ToList();
        }

        public exam_testuser GetModel(string userId, int testId)
        {
            return Db.Context.From<exam_testuser>().Where(q => q.UserId == userId && q.TestId == testId).ToFirst();
        }

        public void Finish(string userId, int testId)
        {
            Db.Context.Update<exam_testuser>(exam_testuser._.HaveTest, 1,
                q => q.TestId == testId && q.UserId == userId);
        }


        public int RemoveByTestId(int testId)
        {
            return Db.Context.Delete<exam_testuser>(q => q.TestId == testId);
        }

        public List<string> GetNoGradeUsersId(int testId)
        {
            return Db.Context.From<exam_testuser>()
                        .Select(s => s.UserId)
                        .Where(q => q.TestId == testId && q.HaveTest == 0)
                        .ToList<string>();

        }
    }
}
