using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NtAbcExam.Domain.Models;
using NtAbcExam.FrameWork.Cache;

namespace NtAbcExam.Domain.Repositories
{
    public class TestPaperRepository
    {
        private readonly ExamTestRepository _examTestRep = new ExamTestRepository();
        private readonly ExamTestUserRepository _examTestUserRep = new ExamTestUserRepository();
        private readonly ExamDataBaseRepository _examDbRep = new ExamDataBaseRepository();
        private readonly ExamScoreRepository _examScore = new ExamScoreRepository();
        private readonly CadreInfoRepository _cadreInfoRep = new CadreInfoRepository();

        private readonly RedisHelper _redis = new RedisHelper(1);

        public TestPaper GetTestPaper(int testId, string userId)
        {
            var testPager = LoadFormCache(testId, userId) ?? GenerateTestPaper(testId, userId);

            return testPager;
        }

        public void FinishTest(string userId, TestPaper testPaper)
        {
            if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentOutOfRangeException(nameof(userId));
            if (testPaper == null) throw new ArgumentNullException(nameof(testPaper));

            var user = _cadreInfoRep.GetModelByUserId(userId);

            if (user == null)
            {
                throw new ArgumentOutOfRangeException(nameof(userId));
            }

            var testResult = testPaper.FinishTest(userId);

            var score = new exam_score()
            {
                UserId = user.UserID,
                UserName = user.UserName,
                DeptId = user.DeptId,
                Office = user.Office,
                Duties = user.Duties,
                SubjectId = testPaper.SubjectId,
                TestId = testPaper.Id,
                StartTime = testPaper.BeginTime,
                EndTime = testPaper.FinishTime
            };

            var sbWrongDan = new StringBuilder();
            var sbWrongDuo = new StringBuilder();
            var sbWrongPd = new StringBuilder();
            score.Score = 0;
            foreach (var result in testResult)
            {
                if (result.Result)
                {
                    score.Score += result.Point;
                }
                else
                {
                    switch (result.Question.Type)
                    {
                        case QuestionType.单选题:
                            sbWrongDan.AppendLine(result.Question.Content);
                            sbWrongDan.AppendLine(QuestionOptionsToText(result.Question));
                            if (result.Answer != null && result.Answer.Options != null && result.Answer.Options.Count > 0)
                            {
                                sbWrongDan.AppendLine("您的答案是：" + string.Join(",", result.Answer.Options));
                            }
                            else
                            {
                                sbWrongDan.AppendLine("您的答案是：");
                            }
                            sbWrongDan.AppendLine("正确答案是：" + string.Join(",", result.Question.RightAnswers));
                            break;
                        case QuestionType.多选题:
                            sbWrongDuo.AppendLine(result.Question.Content);
                            sbWrongDuo.AppendLine(QuestionOptionsToText(result.Question));
                            if (result.Answer != null && result.Answer.Options != null && result.Answer.Options.Count > 0)
                            {
                                sbWrongDuo.AppendLine("您的答案是：" + string.Join(",", result.Answer.Options));
                            }
                            else
                            {
                                sbWrongDuo.AppendLine("您的答案是：");
                            }
                            sbWrongDuo.AppendLine("正确答案是：" + string.Join(",", result.Question.RightAnswers));
                            break;
                        case QuestionType.判断题:
                            sbWrongPd.AppendLine(result.Question.Content);
                            sbWrongPd.AppendLine(QuestionOptionsToText(result.Question));
                            if (result.Answer != null && result.Answer.Options != null && result.Answer.Options.Count > 0)
                            {
                                sbWrongPd.AppendLine("您的答案是：" + string.Join(",", result.Answer.Options));
                            }
                            else
                            {
                                sbWrongPd.AppendLine("您的答案是：");
                            }
                            sbWrongPd.AppendLine("正确答案是：" + string.Join(",", result.Question.RightAnswers));
                            break;
                    }
                }
            }

            score.Wrong_Dan = sbWrongDan.ToString();
            score.Wrong_Duo = sbWrongDuo.ToString();
            score.Wrong_Pd = sbWrongPd.ToString();
            //检查是否已经交过卷，同一个人同一门考试只能有一成绩
            if (!_examScore.IsFinishedExam(score.TestId, score.UserId))
            {
                //最好事务处理....
                _examScore.Add(score);
                _examTestUserRep.Finish(userId, testPaper.Id);
            }
            RemoveFormCache(testPaper.Id, userId);
        }

        public void SaveToCache(int testId, string userId, TestPaper testPaper)
        {
            //SaveToRedis
            string key = "TestPaper_" + testId;
            string dataKey = testId + "-" + userId;
            if (_redis.HashExists(key, dataKey))
            {
                _redis.HashDelete(key, dataKey);
            }

            _redis.HashSet<TestPaper>(key, dataKey, testPaper);
        }


        //
        private TestPaper LoadFormCache(int testId, string userId)
        {
            string key = "TestPaper_" + testId;
            string dataKey = testId + "-" + userId;
            if (_redis.HashExists(key, dataKey))
            {
                return _redis.HashGet<TestPaper>(key, dataKey);
            }
            return null;
        }

        private void RemoveFormCache(int testId, string userId)
        {
            string key = "TestPaper_" + testId;
            string dataKey = testId + "-" + userId;
            if (_redis.HashExists(key, dataKey))
            {
                _redis.HashDelete(key, dataKey);
            }
        }

        private TestPaper GenerateTestPaper(int testId, string userId)
        {
            var examTest = _examTestRep.GetModelByTestId(testId);
            if (examTest == null)
            {
                throw new NullReferenceException();
            }

            var questions = new List<Question>();

            if (examTest.SingleCount > 0)
            {
                questions.AddRange(GenerateQuestions(examTest.DeptId, examTest.SubjectId, QuestionType.单选题, examTest.SingleCount));
            }

            if (examTest.MultiCount > 0)
            {
                questions.AddRange(GenerateQuestions(examTest.DeptId, examTest.SubjectId, QuestionType.多选题, examTest.MultiCount));
            }

            if (examTest.JudgeCount > 0)
            {
                questions.AddRange(GenerateQuestions(examTest.DeptId, examTest.SubjectId, QuestionType.判断题, examTest.JudgeCount));
            }

            var testPaper = new TestPaper(testId, userId, examTest.SubjectId, DateTime.Now, examTest.TestTime,
                examTest.SingleCount, examTest.SinglePer,
                examTest.MultiCount, examTest.MultiPer,
                examTest.JudgeCount, examTest.JudgePer, questions);

            SaveToCache(testId, userId, testPaper);
            return testPaper;
        }

        private List<Question> GenerateQuestions(int deptId, int subjectId, QuestionType type, int count)
        {
            var questions = _examDbRep.GetListByType(type, deptId, subjectId);
            if (count > questions.Count)
            {
                throw new ArgumentOutOfRangeException("题库总数小于需要生成的数量");
            }

            var selectedQuestions = new List<Question>();
            //var badGenerateCount = 0;
            for (int i = 0; i < count; i++)
            {
                //if (badGenerateCount > questions.Count * count) break;

                long tick = DateTime.Now.Ticks;
                Random rnd = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
                var index = rnd.Next(questions.Count);

                var q = questions[index];

                if (selectedQuestions.Count(c => c.Id == q.Id) <= 0)
                {
                    var options = GenerateOptions(q, type);
                    var answers = q.Answer.ToUpper().Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                    var question = new Question(q.Id, q.Question, type, options, answers.ToList());
                    selectedQuestions.Add(question);
                    //badGenerateCount = 0;
                }
                //抽中后移除
                questions.RemoveAt(index);
                //else
                //{
                //    i--;
                //    badGenerateCount++;
                //    continue;
                //}
            }

            return selectedQuestions;
        }

        private static Dictionary<string, string> GenerateOptions(exam_database q, QuestionType type)
        {
            var options = new Dictionary<string, string>();
            if (type == QuestionType.判断题)
            {
                options.Add("Y", "正确");
                options.Add("N", "错误");
                return options;
            }
            int optionName = 65;

            if (!string.IsNullOrWhiteSpace(q.Text1))
            {
                options.Add(Convert.ToChar(optionName++).ToString(), q.Text1.Trim());
            }

            if (!string.IsNullOrWhiteSpace(q.Text2))
            {
                options.Add(Convert.ToChar(optionName++).ToString(), q.Text2.Trim());
            }

            if (!string.IsNullOrWhiteSpace(q.Text3))
            {
                options.Add(Convert.ToChar(optionName++).ToString(), q.Text3.Trim());
            }

            if (!string.IsNullOrWhiteSpace(q.Text4))
            {
                options.Add(Convert.ToChar(optionName++).ToString(), q.Text4.Trim());
            }

            if (!string.IsNullOrWhiteSpace(q.Text5))
            {
                options.Add(Convert.ToChar(optionName++).ToString(), q.Text5.Trim());
            }

            if (!string.IsNullOrWhiteSpace(q.Text6))
            {
                options.Add(Convert.ToChar(optionName).ToString(), q.Text6.Trim());
            }

            return options;
        }

        private static string QuestionOptionsToText(Question question)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var pair in question.Options)
            {
                sb.AppendLine(pair.Key + "、" + pair.Value);
            }
            return sb.ToString();
        }
    }
}
