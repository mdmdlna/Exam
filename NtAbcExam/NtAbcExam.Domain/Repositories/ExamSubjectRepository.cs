using System.Collections.Generic;
using NtAbcExam.Domain.Models;
using NtAbcExam.FrameWork.Abstractions;
using NtAbcExam.FrameWork.Data;

namespace NtAbcExam.Domain.Repositories
{
    public class ExamSubjectRepository : Repository<exam_subject>
    {
        public exam_subject GetModelById(int id)
        {
            return Db.Context.From<exam_subject>().Where(q => q.Id == id).First();
        }
    }
}
