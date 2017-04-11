using System;
using System.Collections.Generic;
using Dos.ORM;
using NtAbcExam.Domain.Models;
using NtAbcExam.FrameWork.Abstractions;
using NtAbcExam.FrameWork.Data;

namespace NtAbcExam.Domain.Repositories
{
    public class ExamTestRepository : Repository<exam_test>
    {
        public exam_test GetModelById(int id)
        {
            return Db.Context.From<exam_test>().Where(q => q.Id == id).First();

        }

        public exam_test GetModelByTestId(int testId)
        {
            return Db.Context.From<exam_test>().Where(q => q.TestId == testId).First();
        }

        public exam_test GetTestList(int testId)
        {
            return Db.Context.From<exam_test>()
                .Where(q => q.TestId == testId && q.StartTime <= DateTime.Now && q.EndTime >= DateTime.Now)
                .ToFirst();
        }

        public bool Add(exam_test test, List<exam_testuser> users)
        {
            var trans = Db.Context.BeginTransaction();
            try
            {
                trans.Insert(test);
                trans.Insert(users);
                trans.Commit();
                return true;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                return false;
            }
            finally
            {
                trans.Close();
            }
        }
    }
}
