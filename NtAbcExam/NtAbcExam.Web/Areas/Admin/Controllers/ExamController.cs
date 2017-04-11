using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Newtonsoft.Json;
using NtAbcExam.Domain.Models;
using NtAbcExam.Domain.Repositories;
using NtAbcExam.FrameWork.Data;
using NtAbcExam.Web.Areas.Admin.Common;
using NtAbcExam.Web.Areas.Admin.Filters;
using NtAbcExam.Web.Areas.Admin.Models;
using NtAbcExam.Web.Models;

namespace NtAbcExam.Web.Areas.Admin.Controllers
{
    [AdminSeesion]
    public class ExamController : Controller
    {
        private readonly ExamDataBaseRepository _examRep = new ExamDataBaseRepository();
        private readonly ExamTestRepository _examTestRep = new ExamTestRepository();
        private readonly ExamScoreRepository _examScoreRep = new ExamScoreRepository();
        private readonly CadreInfoRepository _cadreInfoRep = new CadreInfoRepository();
        private readonly DepartmentRepository _deptRep = new DepartmentRepository();
        private readonly ExamSubjectRepository _subjectRep = new ExamSubjectRepository();
        private readonly ExamTestUserRepository _examTestUserRep = new ExamTestUserRepository();

        // GET: Admin/Exam
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ExamTestIndex()
        {
            return View();
        }

        public ActionResult ViewScore(int testId)
        {
            ViewBag.TestId = testId;
            return View();
        }


        public ActionResult EmpList(int deptId)
        {
            var cadreInfos = _cadreInfoRep.GetListByDeptId(deptId);
            var emps = Mapper.Map<List<cadre_info>, List<EmpViewModel>>(cadreInfos);
            return View(emps);
        }

        public ActionResult CreateExam()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateExam(ExamTestViewModel viewModel)
        {
            ResultModel result = new ResultModel();

            exam_test test = new exam_test();
            Mapper.Map(viewModel, test);
            TimeSpan ts = DateTime.UtcNow - new DateTime(2010, 1, 1, 0, 0, 0, 0);
            test.TestId = Convert.ToInt32(ts.TotalSeconds);
            test.TotalPer = test.SingleCount * test.SinglePer + test.MultiCount * test.MultiPer + test.JudgeCount * test.JudgePer;
            test.SetTIme = DateTime.Now;
            var emps = viewModel.EmpIds.Replace("\r\n", ",");
            List<string> empList = emps.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
            List<exam_testuser> testUsers = new List<exam_testuser>();
            foreach (var emp in empList)
            {
                exam_testuser users = new exam_testuser
                {
                    HaveTest = 0,
                    TestId = test.TestId,
                    UserId = emp

                };
                testUsers.Add(users);
            }

            result.IsSuccess = _examTestRep.Add(test, testUsers);
            return Json(result);
        }

        public ActionResult Import()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetList(ModelQuery modelQuery)
        {
            //List<exam_database> examAll = _examRep.GetAll();

            List<ModelFilter> filters = JsonConvert.DeserializeObject<List<ModelFilter>>(modelQuery.filters);
            var deptFilter = filters.Find(f => f.name == "DeptId");
            if (deptFilter != null && deptFilter.value == "-1")
            {
                filters.Remove(deptFilter);
            }
            var subjectFilter = filters.Find(f => f.name == "SubjectId");
            if (subjectFilter != null && subjectFilter.value == "-1")
            {
                filters.Remove(subjectFilter);
            }

            modelQuery.filters = JsonConvert.SerializeObject(filters);


            int totalRow;
            var exams = _examRep.Query(modelQuery, out totalRow);
            var empViewModels = Mapper.Map<List<exam_database>, List<ExamQuestionViewModel>>(exams);

            GridModel grid = new GridModel();
            grid.total = totalRow;
            grid.rows = empViewModels;
            return Content(JsonConvert.SerializeObject(grid));
        }

        [HttpPost]
        public ActionResult GetExamTestList(ModelQuery modelQuery)
        {
            var depts = _deptRep.GetAll();
            var subjects = _subjectRep.GetAll();

            var filters = JsonConvert.DeserializeObject<List<ModelFilter>>(modelQuery.filters);
            var deptFilter = filters.Find(f => f.name == "DeptId");
            if (deptFilter != null && deptFilter.value == "-1")
            {
                filters.Remove(deptFilter);
            }
            var subjectFilter = filters.Find(f => f.name == "SubjectId");
            if (subjectFilter != null && subjectFilter.value == "-1")
            {
                filters.Remove(subjectFilter);
            }
            modelQuery.filters = JsonConvert.SerializeObject(filters);
            modelQuery.ordername = "SetTIme";
            modelQuery.order = "Desc";

            int totalRow;
            var tests = _examTestRep.Query(modelQuery, out totalRow);
            var examTestViewModels = Mapper.Map<List<exam_test>, List<ExamTestViewModel>>(tests);

            //这儿应该用视图的，不应该用这个方法找的，但是数据少，。。。。
            foreach (var viewModel in examTestViewModels)
            {
                var dept = depts.Find(f => f.Id == viewModel.DeptId);
                viewModel.DeptName = dept.DeptName;

                var subject = subjects.Find(f => f.Id == viewModel.SubjectId);
                viewModel.SubjectName = subject.SubjectName;
            }

            GridModel grid = new GridModel();
            grid.total = totalRow;
            grid.rows = examTestViewModels;
            return Content(JsonConvert.SerializeObject(grid));
        }

        [HttpPost]
        public ActionResult GetScoreList(ModelQuery modelQuery)
        {
            List<department> depts = _deptRep.GetAll();
            List<exam_subject> subjects = _subjectRep.GetAll();

            List<ModelFilter> filters = JsonConvert.DeserializeObject<List<ModelFilter>>(modelQuery.filters);
            var deptFilter = filters.Find(f => f.name == "DeptId");
            if (deptFilter != null && deptFilter.value == "-1")
            {
                filters.Remove(deptFilter);
            }
            var subjectFilter = filters.Find(f => f.name == "SubjectId");
            if (subjectFilter != null && subjectFilter.value == "-1")
            {
                filters.Remove(subjectFilter);
            }
            modelQuery.filters = JsonConvert.SerializeObject(filters);
            modelQuery.ordername = "EndTime";
            modelQuery.order = "Desc";

            int totalRow;
            var scores = _examScoreRep.Query(modelQuery, out totalRow);
            var scoreViewModels = Mapper.Map<List<exam_score>, List<ExamScoreViewModel>>(scores);

            //这儿应该用视图的，不应该用这个方法找的，但是数据少，。。。。
            foreach (var viewModel in scoreViewModels)
            {
                var dept = depts.Find(f => f.Id == viewModel.DeptId);
                viewModel.DeptName = dept.DeptName;

                var subject = subjects.Find(f => f.Id == viewModel.SubjectId);
                viewModel.SubjectName = subject.SubjectName;
            }

            GridModel grid = new GridModel();
            grid.total = totalRow;
            grid.rows = scoreViewModels;
            return Content(JsonConvert.SerializeObject(grid));
        }

        [HttpPost]

        public ActionResult Import(HttpPostedFileBase fileBase)
        {
            var file = Request.Files["files"];
            if (file == null || file.ContentLength <= 0)
            {
                ViewBag.Error = "文件不能为空";
                return View();
            }

            int subjectId = int.Parse(Request.Form["SubjectId"]);
            int deptId = int.Parse(Request.Form["DeptId"]);

            var filename = Path.GetFileName(file.FileName);
            var fileEx = Path.GetExtension(filename); //获取上传文件的扩展名
            var noFileName = Path.GetFileNameWithoutExtension(filename); //获取无扩展名的文件名
            const string fileType = ".xls,.xlsx"; //定义上传文件的类型字符串
            var newFileName = noFileName + DateTime.Now.ToString("yyyyMMddhhmmss") + fileEx;
            if (fileEx != null && !fileType.Contains(fileEx))
            {
                ViewBag.Error = "文件类型不对，只能导入xls和xlsx格式的文件";
                return View();
            }
            string path = AppDomain.CurrentDomain.BaseDirectory + "uploads/excel/";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string savePath = Path.Combine(path, newFileName);
            file.SaveAs(savePath);

            var expImp = new ExportImport();
            var dt = expImp.GetFillData(savePath, false);
            if (dt == null)
            {
                ViewBag.Error = "导入的数据文件格式不正确";
                return View();
            }
            var importCount = dt.Rows.Count;
            if (importCount == 0)
            {
                ViewBag.Error = "导入的数据文件为空";
                return View();
            }

            string message;
            if (!CheckExcel(ref dt, out message))
            {
                ViewBag.Error = "!!!导入失败!!!<br/>" + message;
                return View();
            }

            if (dt.Rows.Count == 0)
            {
                ViewBag.Error = "无正确的数据可导入";
                return View();
            }

            var examDb = new List<exam_database>();


            foreach (DataRow dr in dt.Rows)
            {
                var exam = new exam_database()
                {
                    Type = dr[0].ToString(),
                    Question = dr[1].ToString(),
                    Text1 = dr[2].ToString(),
                    Text2 = dr[3].ToString(),
                    Text3 = dr[4].ToString(),
                    Text4 = dr[5].ToString(),
                    Text5 = dr[6].ToString(),
                    Text6 = dr[7].ToString(),
                    Answer = dr[8].ToString().ToUpper(),
                    SubjectId = subjectId,
                    DeptId = deptId

                };
                examDb.Add(exam);
            }

            //写库
            bool ret = _examRep.Add(examDb) > 0;
            ViewBag.Error = ret ? "导入成功" : "导入失败";

            return View();
        }

        public ActionResult Del(int id)
        {
            var result = new ResultModel()
            {
                IsSuccess = false
            };

            var question = _examRep.GetModelById(id);
            if (question != null)
            {
                result.IsSuccess = _examRep.Remove(question) > 0;
            }

            return Json(result);
        }

        public ActionResult DelTest(int id)
        {
            var result = new ResultModel
            {
                IsSuccess = false
            };

            var test = _examTestRep.GetModelById(id);
            if (test != null)
            {
                _examTestUserRep.RemoveByTestId(test.TestId);
                result.IsSuccess = _examTestRep.Remove(test) > 0;
            }

            return Json(result);
        }

        public ActionResult DelScore(int id)
        {
            ResultModel result = new ResultModel();
            result.IsSuccess = false;
            var score = _examScoreRep.GetModelById(id);
            if (score != null)
            {
                result.IsSuccess = _examScoreRep.Remove(score) > 0;
            }

            return Json(result);
        }

        private bool CheckExcel(ref DataTable dt, out string log)
        {
            var sb = new StringBuilder();
            var isSuccess = true;
            try
            {
                var count = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    //var dr = dt.Rows[count];
                    var rowNum = count + 2;

                    if (string.IsNullOrWhiteSpace(dr[0]?.ToString()))
                    {
                        isSuccess = false;
                        sb.AppendFormat("第{0}行，题目类型为空。<br/>", rowNum);
                        //dt.Rows.Remove(dr);
                        //continue;
                    }
                    else if (dr[0].ToString().Trim() != "单选题" && dr[0].ToString().Trim() != "多选题" && dr[0].ToString().Trim() != "判断题")
                    {
                        isSuccess = false;
                        sb.AppendFormat("第{0}行，题目类型取值不是：【单选题】【多选题】【判断题】其中之一。<br/>", rowNum);
                        //dt.Rows.Remove(dr);
                        //continue;
                    }

                    if (string.IsNullOrWhiteSpace(dr[1]?.ToString()))
                    {
                        isSuccess = false;
                        sb.AppendFormat("第{0}行，题目正文为空。<br/>", rowNum);
                        //dt.Rows.Remove(dr);
                        //continue;
                    }

                    if (string.IsNullOrWhiteSpace(dr[8]?.ToString()))
                    {
                        isSuccess = false;
                        sb.AppendFormat("第{0}行，答案为空<br/>", rowNum);
                        //dt.Rows.Remove(dr);
                    }
                    else if (dr[0] != null && dr[0].ToString() == "多选题" && dr[8].ToString().Length > 1 && dr[8].ToString().IndexOf(",", StringComparison.Ordinal) < 0)
                    {
                        isSuccess = false;
                        sb.AppendFormat("第{0}行，多个答案必须用英文逗号(,)分隔<br/>", rowNum);
                        //dt.Rows.Remove(dr);
                    }
                    else if (dr[0] != null && dr[0].ToString() == "判断题" && dr[8].ToString().ToUpper() != "Y" && dr[8].ToString().ToUpper() != "N")
                    {
                        isSuccess = false;
                        sb.AppendFormat("第{0}行，判断题的答案只能是Y或N<br/>", rowNum);
                        //dt.Rows.Remove(dr);
                    }
                    count++;
                }
                log = sb.ToString();
            }
            catch (Exception ex)
            {
                log = "非系统导入模板==>" + ex.Message;
                return false;
            }
            return isSuccess;
        }



    }
}