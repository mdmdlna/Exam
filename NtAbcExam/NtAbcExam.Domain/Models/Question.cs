using System;
using System.Collections.Generic;
using System.Linq;

namespace NtAbcExam.Domain.Models
{
    public class Question
    {
        public int Id { get; private set; }

        public string Content { get; private set; }

        public QuestionType Type { get; private set; }

        public Dictionary<string, string> Options { get; private set; }

        public List<string> RightAnswers { get; private set; } = new List<string>();

        public Question(int id, string content, QuestionType type, Dictionary<string, string> options, List<string> rightAnswers)
        {
            if (id <= 0) throw new ArgumentNullException(nameof(id));
            if (string.IsNullOrWhiteSpace(content)) throw new ArgumentNullException(nameof(content));
            if (options == null) throw new ArgumentNullException(nameof(options));
            if (options.Count <= 0) throw new ArgumentNullException(nameof(options));

            Id = id;
            Content = content;
            Options = options;
            Type = type;

            RightAnswers.Clear();
            RightAnswers.AddRange(rightAnswers);
        }

        public bool IsRight(Answer answer)
        {
            if (answer == null) throw new ArgumentNullException(nameof(answer));
            if (answer.QuestionId != Id)
            {
                throw new ArgumentNullException(nameof(answer));
            }

            //答案个数不等
            if (RightAnswers.Count != answer.Options?.Count)
            {
                return false;
            }

            //转换为大写
            var rightAnswers = RightAnswers.Select(s => s.ToUpper());
            var answerOptions = answer.Options.Select(s => s.ToUpper());

            return !(rightAnswers.Except(answerOptions)).Any();
        }
    }


    public enum QuestionType
    {
        单选题 = 1,
        多选题 = 2,
        判断题 = 3
    }
}
