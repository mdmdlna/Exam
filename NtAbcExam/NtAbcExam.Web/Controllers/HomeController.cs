using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using NtAbcExam.Domain.Models;
using NtAbcExam.Domain.Repositories;
using NtAbcExam.Web.Common;
using NtAbcExam.Web.Filters;
using NtAbcExam.Web.Models;

namespace NtAbcExam.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ExamTestUserRepository _examTestUserRep = new ExamTestUserRepository();
        private readonly ExamTestRepository _examTestRep = new ExamTestRepository();
        private readonly TestPaperRepository _testPaperRep = new TestPaperRepository();
        private readonly ExamScoreRepository _examScoreRep = new ExamScoreRepository();
        private readonly CadreInfoRepository _cadreInfoRep = new CadreInfoRepository();
        private readonly ExamSubjectRepository _subjectRep = new ExamSubjectRepository();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel viewModel)
        {
            if (!UserLoginHelper.Login(viewModel.UserId, viewModel.UserPwd))
            {
                ModelState.AddModelError("", "账号或密码不正确");
                return View();
            }

            return RedirectToAction("SelectTest");
        }

        [UserSeesion]
        public ActionResult SelectTest()
        {
            var subjects = _subjectRep.GetAll();
            var userId = UserLoginHelper.LoginInfo.UserID;
            var examViewModels = new List<ExamViewModel>();
            var myTests = _examTestUserRep.GetMyTest(userId);
            foreach (var test in myTests)
            {
                var my = _examTestRep.GetTestList(test.TestId);
                if (my != null)
                {
                    var model = new ExamViewModel();
                    Mapper.Map(my, model);
                    var s = subjects.Find(f => f.Id == my.SubjectId);
                    model.SubjectName = s.SubjectName;
                    examViewModels.Add(model);
                }
            }
            return View(examViewModels);
        }

        [UserSeesion]
        public ActionResult Start(int testId)
        {
            string userId = UserLoginHelper.LoginInfo.UserID;

            var userTestInfo = _examTestUserRep.GetModel(userId, testId);
            if (userTestInfo == null)
            {
                return RedirectToAction("NoExamError");
            }

            if (userTestInfo.HaveTest == 1)
            {
                return RedirectToAction("Result", new { testId = userTestInfo.TestId });
            }

            var testPaper = _testPaperRep.GetTestPaper(testId, userId);

            if ((DateTime.Now - testPaper.BeginTime).TotalMinutes > testPaper.TestDuration)
            {
                //超时就直接交卷
                _testPaperRep.FinishTest(userId, testPaper);
                return RedirectToAction("TimeOutError");
            }

            var question = new QuestionViewModel();

            //Mapper.Map(testPaper.Questions[0], question);

            question.Index = 0;
            question.Total = testPaper.Questions.Count;
            question.TestId = testId;
            question.Balance = (testPaper.TestDuration * 60) - Convert.ToInt32((DateTime.Now - testPaper.BeginTime).TotalSeconds);
            question.SingleCount = testPaper.SingleCount;
            question.MultiCount = testPaper.MultiCount;
            question.JudgeCount = testPaper.JudgeCount;

            ViewBag.Total = testPaper.Questions.Count;
            return View(question);
        }

        [UserSeesion]
        public ActionResult GoIndex(int testId, int index, int? questionId, List<string> answerOptions)
        {
            string userId = UserLoginHelper.LoginInfo.UserID;
            var testPaper = _testPaperRep.GetTestPaper(testId, userId);

            if ((DateTime.Now - testPaper.BeginTime).TotalMinutes > testPaper.TestDuration)
            {
                //如果超时，直接交卷
                _testPaperRep.FinishTest(userId, testPaper);
                return PartialView("TimeOutErrorNoLayout");
            }

            if (answerOptions != null && questionId != null && questionId > 0)
            {
                //var answer = answerOptions.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
                answerOptions = answerOptions.Select(s => s.ToUpper()).ToList();
                testPaper.AddAnswer(questionId.Value, answerOptions);
                _testPaperRep.SaveToCache(testId, userId, testPaper);
            }

            var question = new QuestionViewModel();
            Mapper.Map(testPaper.GoIndex(index), question);

            question.Index = index;
            question.Total = testPaper.Questions.Count;
            question.TestId = testId;
            ViewBag.Total = testPaper.Questions.Count;
            question.Balance = (testPaper.TestDuration * 60) - Convert.ToInt32((DateTime.Now - testPaper.BeginTime).TotalSeconds);
            question.SingleCount = testPaper.SingleCount;
            question.MultiCount = testPaper.MultiCount;
            question.JudgeCount = testPaper.JudgeCount;

            var ops = testPaper.Answers.Find(f => f.QuestionId == question.Id);
            if (ops != null)
            {
                question.Answers = ops.Options;
            }

            return PartialView("_Question", question);
        }

        [UserSeesion]
        public ActionResult Finish(int testId, int index, int? questionId, List<string> answerOptions)
        {
            string userId = UserLoginHelper.LoginInfo.UserID;
            var testPaper = _testPaperRep.GetTestPaper(testId, userId);
            // 
            if ((DateTime.Now - testPaper.BeginTime).TotalMinutes > testPaper.TestDuration)
            {
                //如果超时，直接交卷
                _testPaperRep.FinishTest(userId, testPaper);
                return RedirectToAction("TimeOutError");
            }

            //没答题
            //if (answerOptions == null)
            //{
            //    return RedirectToAction("NoAnswerError");
            //}

            if (questionId != null && questionId > 0)
            {
                answerOptions = answerOptions.Select(s => s.ToUpper()).ToList();
                testPaper.AddAnswer(questionId.Value, answerOptions);
                _testPaperRep.SaveToCache(testId, userId, testPaper);
                _testPaperRep.FinishTest(userId, testPaper);
            }

            return RedirectToAction("Result", new { testId = testId });
        }

        [UserSeesion]
        public ActionResult Result(int testId)
        {
            string userId = UserLoginHelper.LoginInfo.UserID;
            var examScore = _examScoreRep.GetModel(testId, userId);
            var resultViewModel = new ResultViewModel();
            if (examScore != null)
            {
                resultViewModel.BeginTime = examScore.StartTime ?? DateTime.Now;
                resultViewModel.EndTime = examScore.EndTime ?? DateTime.Now;
                resultViewModel.Point = examScore.Score;
            }
            return View(resultViewModel);
        }

        [UserSeesion]
        public ActionResult Logout()
        {
            UserLoginHelper.Logout();
            return RedirectToAction("Login", "Home");
        }

        public ActionResult NoExamError()
        {
            return View();
        }

        public ActionResult TimeOutError()
        {
            return View();
        }

        public ActionResult NoAnswerError()
        {
            return View();
        }
    }
}