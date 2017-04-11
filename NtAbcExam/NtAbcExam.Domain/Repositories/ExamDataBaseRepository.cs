using System.Collections.Generic;
using NtAbcExam.Domain.Models;
using NtAbcExam.FrameWork.Abstractions;
using NtAbcExam.FrameWork.Data;

namespace NtAbcExam.Domain.Repositories
{
    public class ExamDataBaseRepository : Repository<exam_database>
    {
        public exam_database GetModelById(int id)
        {
            return Db.Context.From<exam_database>().Where(q => q.Id == id).First();

        }
        //public List<exam_database> GetListByType(QuestionType type)
        //{
        //    switch (type)
        //    {
        //        case QuestionType.单选题:
        //            return Db.Context.From<exam_database>().Where(q => q.Type == "单选题").ToList();
        //        case QuestionType.多选题:
        //            return Db.Context.From<exam_database>().Where(q => q.Type == "多选题").ToList();
        //        case QuestionType.判断题:
        //            return Db.Context.From<exam_database>().Where(q => q.Type == "判断题").ToList();
        //        default:
        //            return base.GetAll();
        //    }
        //}

        public List<exam_database> GetListByType(QuestionType type, int deptId, int subjectId)
        {
            switch (type)
            {
                case QuestionType.单选题:
                    return Db.Context.From<exam_database>().Where(q => q.Type == "单选题" && q.SubjectId == subjectId && q.DeptId == deptId).ToList();
                case QuestionType.多选题:
                    return Db.Context.From<exam_database>().Where(q => q.Type == "多选题" && q.SubjectId == subjectId && q.DeptId == deptId).ToList();
                case QuestionType.判断题:
                    return Db.Context.From<exam_database>().Where(q => q.Type == "判断题" && q.SubjectId == subjectId && q.DeptId == deptId).ToList();
                default:
                    return base.GetAll();
            }
        }
    }
}
