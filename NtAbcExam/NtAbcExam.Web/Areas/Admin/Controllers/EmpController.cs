using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Newtonsoft.Json;
using NtAbcExam.Domain.Models;
using NtAbcExam.Domain.Repositories;
using NtAbcExam.FrameWork.Data;
using NtAbcExam.FrameWork.Encryption;
using NtAbcExam.Web.Areas.Admin.Common;
using NtAbcExam.Web.Areas.Admin.Filters;
using NtAbcExam.Web.Areas.Admin.Models;
using NtAbcExam.Web.Models;

namespace NtAbcExam.Web.Areas.Admin.Controllers
{
    [AdminSeesion]
    public class EmpController : Controller
    {
        private readonly CadreInfoRepository _cadreInfoRep = new CadreInfoRepository();
        private readonly DepartmentRepository _deptRep = new DepartmentRepository();

        // GET: Admin/Emp
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            var viewModel = new EmpViewModel();
            viewModel.DeptSelectList = new SelectList(_deptRep.GetAll(), "Id", "DeptName");
            return View(viewModel);
        }

        public ActionResult Edit(string userId)
        {
            var emp = _cadreInfoRep.GetModelByUserId(userId);
            var viewModel = new EmpViewModel();
            Mapper.Map(emp, viewModel);
            viewModel.DeptSelectList = new SelectList(_deptRep.GetAll(), "Id", "DeptName");

            return View(viewModel);
        }

        public ActionResult Import(string aa)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Import(HttpPostedFileBase fileBase)
        {
            var file = Request.Files["files"];
            //int deptId = int.Parse(Request.Form["DeptId"]);

            if (file == null || file.ContentLength <= 0)
            {
                ViewBag.error = "文件不能为空";
                return View();
            }

            var filename = Path.GetFileName(file.FileName);
            var fileEx = Path.GetExtension(filename); //获取上传文件的扩展名
            var noFileName = Path.GetFileNameWithoutExtension(filename); //获取无扩展名的文件名
            const string fileType = ".xls,.xlsx"; //定义上传文件的类型字符串
            var newFileName = noFileName + DateTime.Now.ToString("yyyyMMddhhmmss") + fileEx;
            if (fileEx != null && !fileType.Contains(fileEx))
            {
                ViewBag.error = "文件类型不对，只能导入xls和xlsx格式的文件";
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
                ViewBag.Error = "导入失败";
                return View();
            }
            if (dt.Rows.Count == 0)
            {
                ViewBag.Error = "无正确的数据可导入";
                return View();
            }

            var empList = new List<cadre_info>();
            foreach (DataRow dr in dt.Rows)
            {
                cadre_info emp = new cadre_info
                {
                    UserID = dr[0].ToString(),
                    UserName = dr[1].ToString(),
                    Pwd = dr[2].ToString(),
                    DeptId = AdminLoginHelper.LoginInfo.DeptId
                };

                empList.Add(emp);
            }

            //写库
            bool ret = _cadreInfoRep.Add(empList) > 0;
            ViewBag.Error = ret ? "导入成功" : "导入失败";

            return View();
        }

        private bool CheckExcel(ref DataTable dt, out string log)
        {
            log = "";
            bool isSuccess = true;
            try
            {
                for (var i = dt.Rows.Count - 1; i > -1; i--)
                {
                    var dr = dt.Rows[i];

                    if (dr[0] == null || dr[0].ToString().Trim() == "")
                    {
                        //isSuccess = false;
                        log = "第 " + (i + 2) + " 行，，此行被忽略。\r\n" + log;
                        dt.Rows.Remove(dr);
                        continue;
                    }

                    if (dr[1] == null || dr[1].ToString().Trim() == "")
                    {
                        //isSuccess = false;
                        log = "第 " + (i + 2) + " 行，，此行被忽略。\r\n" + log;
                        dt.Rows.Remove(dr);
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                log = "非系统导入模板==>" + ex.Message;
                return false;
            }
            return isSuccess;
        }

        [HttpPost]
        public ActionResult GetList(ModelQuery modelQuery)
        {
            List<department> depts = _deptRep.GetAll();

            List<ModelFilter> filters = JsonConvert.DeserializeObject<List<ModelFilter>>(modelQuery.filters);
            modelQuery.filters = JsonConvert.SerializeObject(filters);

            int totalRow = 0;
            var cadreInfos = _cadreInfoRep.Query(modelQuery, out totalRow);
            var empViewModels = Mapper.Map<List<cadre_info>, List<EmpViewModel>>(cadreInfos);

            //这儿应该用视图的，不应该用这个方法找的，但是数据少，。。。。
            foreach (var viewModel in empViewModels)
            {
                var dept = depts.Find(f => f.Id == viewModel.DeptId);
                viewModel.DeptName = dept.DeptName;
            }

            GridModel grid = new GridModel();
            grid.total = totalRow;
            grid.rows = empViewModels;
            return Content(JsonConvert.SerializeObject(grid));
        }

        [HttpPost]
        public ActionResult AddNew(EmpViewModel viewModel)
        {
            ResultModel result = new ResultModel();
            if (_cadreInfoRep.Any(q => q.UserID == viewModel.UserId))
            {
                result.IsSuccess = false;
                result.Msg = "身份证号码已存在";
                Json(result);
            }

            cadre_info adminUser = new cadre_info();
            Mapper.Map(viewModel, adminUser);
            result.IsSuccess = _cadreInfoRep.Add(adminUser) > 0;
            return Json(result);
        }

        [HttpPost]
        public ActionResult EditSave(EmpViewModel viewModel)
        {
            ResultModel result = new ResultModel();
            if (_cadreInfoRep.Any(q => q.Id != viewModel.Id && q.UserID == viewModel.UserId))
            {
                result.IsSuccess = false;
                result.Msg = "身份证号码已存在";
                Json(result);
            }
            cadre_info cadreInfo = _cadreInfoRep.GetModelByUserId(viewModel.UserId);
            Mapper.Map(viewModel, cadreInfo);
            result.IsSuccess = _cadreInfoRep.Update(cadreInfo) > 0;
            return Json(result);
        }

        public ActionResult Del(int id)
        {
            ResultModel result = new ResultModel();
            result.IsSuccess = false;
            var adminUser = _cadreInfoRep.GetModelById(id);
            if (adminUser != null)
            {
                result.IsSuccess = _cadreInfoRep.Remove(adminUser) > 0;
            }

            return Json(result);
        }

        public ActionResult IsExistUserId(int id, string userId)
        {
            return Json(!_cadreInfoRep.Any(q => q.Id != id && q.UserID == userId));
        }

        public ActionResult ResetPwd(string userId)
        {
            ResultModel result = new ResultModel();
            result.IsSuccess = _cadreInfoRep.ChangePwd(userId, Md5.Compute("111111"));
            return Json(result);
        }
    }
}