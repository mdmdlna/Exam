using System;
using System.Collections.Generic;

namespace NtAbcExam.Domain.Models
{
    public class Answer
    {
        public string Id { get; private set; }

        public int QuestionId { get; private set; }

        public List<string> Options { get; private set; }

        public Answer(int questionId, List<string> options)
        {
            if (questionId <= 0) throw new ArgumentNullException(nameof(questionId));
            //if (options == null) throw new ArgumentNullException(nameof(options));
            //if (options.Count <= 0) throw new ArgumentNullException(nameof(options));

            Id = Guid.NewGuid().ToString();
            QuestionId = questionId;
            Options = options;
        }

        public void Update(List<string> options)
        {
            //if (options == null) throw new ArgumentNullException(nameof(options));
            //if (options.Count <= 0) throw new ArgumentNullException(nameof(options));

            Options = options;
        }
    }
}
