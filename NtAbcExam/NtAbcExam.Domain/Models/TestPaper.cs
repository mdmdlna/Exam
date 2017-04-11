using System;
using System.Collections.Generic;
using System.Linq;

namespace NtAbcExam.Domain.Models
{
    public class TestPaper
    {
        public int Id { get; private set; }

        public string UserId { get; private set; }

        public int SubjectId { get; private set; }

        public DateTime BeginTime { get; private set; }

        public DateTime FinishTime { get; private set; }

        /// <summary>
        /// 考试时长
        /// </summary>
        public int TestDuration { get; private set; }

        /// <summary>
        /// 试卷总分
        /// </summary>
        public decimal TotalPer
        {
            get
            {
                return this.SingleCount * this.SingPer +
                       this.MultiCount * this.MultiPer +
                       this.JudgeCount * this.JudgePer;
            }
        }

        /// <summary>
        /// 单选题条数
        /// </summary>
        public int SingleCount { get; private set; }

        /// <summary>
        /// 单选题单条分数
        /// </summary>
        public decimal SingPer { get; private set; }

        /// <summary>
        /// 多选题条数
        /// </summary>
        public int MultiCount { get; private set; }

        /// <summary>
        /// 多选题单条分数
        /// </summary>
        public decimal MultiPer { get; private set; }

        /// <summary>
        /// 判断题条数
        /// </summary>
        public int JudgeCount { get; private set; }

        /// <summary>
        /// 判断题单条分数
        /// </summary>
        public decimal JudgePer { get; private set; }

        public List<Question> Questions { get; set; } = new List<Question>();

        public List<Answer> Answers { get; set; } = new List<Answer>();

        public TestPaper(int id, string userId, int subjectId, DateTime beginTime, int testDuration, int singleCount,
            decimal singPer, int multiCount, decimal multiPer, int judgeCount, decimal judgePer, List<Question> questions)
        {
            if (id <= 0) throw new ArgumentNullException(nameof(id));
            if (questions == null) throw new ArgumentNullException(nameof(questions));
            if (questions.Count <= 0) throw new ArgumentNullException(nameof(questions));

            Id = id;
            UserId = userId;
            SubjectId = subjectId;
            BeginTime = beginTime;
            TestDuration = testDuration;
            SingleCount = singleCount;
            SingPer = singPer;
            MultiCount = multiCount;
            MultiPer = multiPer;
            JudgeCount = judgeCount;
            JudgePer = judgePer;


            Questions.Clear();
            Questions.AddRange(questions);
        }

        public Question GoIndex(int currentQuestionIndex)
        {
            if (currentQuestionIndex <= 0)
            {
                return Questions[0];
            }

            if (currentQuestionIndex >= this.Questions.Count)
            {
                return Questions[currentQuestionIndex];
            }

            return Questions[currentQuestionIndex];
        }

        public void AddAnswer(int questionId, List<string> options)
        {
            if (Answers.Count(f => f.QuestionId == questionId) > 0)
            {
                ChangeAnswer(questionId, options);
            }
            else
            {
                var answer = new Answer(questionId, options);
                Answers.Add(answer);
            }
        }

        public void ChangeAnswer(int questionId, List<string> options)
        {
            var answer = Answers.Find(f => f.QuestionId == questionId);
            answer.Update(options);
        }

        public List<TestResult> FinishTest(string userId)
        {
            if (userId != this.UserId) throw new ArgumentException(nameof(userId));

            FinishTime = DateTime.Now;
            var testResult = new List<TestResult>();
            foreach (var question in this.Questions)
            {
                TestResult result;
                var answer = this.Answers.Find(f => f.QuestionId == question.Id);
                if (answer != null)
                {
                    if (question.IsRight(answer))
                    {
                        switch (question.Type)
                        {
                            case QuestionType.单选题:
                                result = new TestResult(question, answer, true, SingPer);
                                break;
                            case QuestionType.多选题:
                                result = new TestResult(question, answer, true, MultiPer);
                                break;
                            case QuestionType.判断题:
                                result = new TestResult(question, answer, true, JudgePer);
                                break;
                            default:
                                throw new Exception("未定义的题目类型");
                        }
                    }
                    else
                    {
                        result = new TestResult(question, answer, false, 0);
                    }
                }
                else
                {
                    result = new TestResult(question, null, false, 0);
                }
                testResult.Add(result);
            }

            return testResult;
        }

        public bool IsAnswerAll()
        {
            return Answers.Count == Questions.Count;
        }
    }
}