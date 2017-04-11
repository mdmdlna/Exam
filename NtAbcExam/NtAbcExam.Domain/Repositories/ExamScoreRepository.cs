using NtAbcExam.Domain.Models;
using NtAbcExam.FrameWork.Abstractions;
using NtAbcExam.FrameWork.Data;

namespace NtAbcExam.Domain.Repositories
{
    public class ExamScoreRepository : Repository<exam_score>
    {
        public exam_score GetModelById(int id)
        {
            return Db.Context.From<exam_score>()
              .Where(q => q.Id == id)
              .ToFirst();
        }

        public exam_score GetModel(int testId, string userId)
        {
            return Db.Context.From<exam_score>()
                .Where(q => q.TestId == testId && q.UserId == userId)
                .ToFirst();
        }

        public bool IsFinishedExam(int testId, string userId)
        {
            return Db.Context.Exists<exam_score>(q=>q.TestId==testId && q.UserId==userId);
        }
    }
}
