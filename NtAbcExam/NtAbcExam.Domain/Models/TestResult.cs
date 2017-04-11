
using System;

namespace NtAbcExam.Domain.Models
{
    public class TestResult
    {
        public TestResult(Question question, Answer answer, bool result, decimal point)
        {
            if (question == null) throw new ArgumentNullException(nameof(question));
            //if (answer == null) throw new ArgumentNullException(nameof(answer));
            if (point < 0) throw new ArgumentOutOfRangeException(nameof(point));

            Question = question;
            Answer = answer;
            Result = result;
            Point = point;
        }

        public Question Question { get; private set; }

        public Answer Answer { get; private set; }

        public bool Result { get; private set; }

        public decimal Point { get; private set; }
    }
}
