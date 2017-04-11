using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Newtonsoft.Json;
using NtAbcExam.Domain.DataModels;
using NtAbcExam.Domain.Models;
using NtAbcExam.Domain.Repositories;
using NtAbcExam.FrameWork.Data;
using NtAbcExam.FrameWork.Encryption;
using NtAbcExam.Web.Areas.Admin.Filters;
using NtAbcExam.Web.Areas.Admin.Models;
using NtAbcExam.Web.Models;

namespace NtAbcExam.Web.Areas.Admin.Controllers
{
    [AdminSeesion]
    public class AdminUserController : Controller
    {
        private readonly AdminUserRepository _adminUserRep = new AdminUserRepository();
        private readonly DepartmentRepository _deptRep = new DepartmentRepository();


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            var viewModel = new AdminUserViewModel();
            viewModel.DeptSelectList = new SelectList(_deptRep.GetAll(), "Id", "DeptName");
            return View(viewModel);
        }

        public ActionResult Edit(int id)
        {
            AdminUser adminUser = _adminUserRep.GetModelById(id);
            AdminUserViewModel viewModel = new AdminUserViewModel();
            Mapper.Map(adminUser, viewModel);
            viewModel.DeptSelectList = new SelectList(_deptRep.GetAll(), "Id", "DeptName");
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult GetList(ModelQuery modelQuery)
        {
            List<department> depts = _deptRep.GetAll();

            List<ModelFilter> filters = JsonConvert.DeserializeObject<List<ModelFilter>>(modelQuery.filters);
            modelQuery.filters = JsonConvert.SerializeObject(filters);

            int totalRow = 0;
            var adminUserList = _adminUserRep.Query(modelQuery, out totalRow);
            var adminUserViewModels = Mapper.Map<List<AdminUser>, List<AdminUserViewModel>>(adminUserList);

            //这儿应该用视图的，不应该用这个方法找的，但是数据少，。。。。
            foreach (var viewModel in adminUserViewModels)
            {
                var dept = depts.Find(f => f.Id == viewModel.DeptId);
                viewModel.DeptName = dept.DeptName;
            }

            GridModel grid = new GridModel();
            grid.total = totalRow;
            grid.rows = adminUserViewModels;
            return Content(JsonConvert.SerializeObject(grid));
        }

        [HttpPost]
        public ActionResult AddNew(AdminUserViewModel viewModel)
        {
            ResultModel result = new ResultModel();
            AdminUser adminUser = new AdminUser();
            Mapper.Map(viewModel, adminUser);
            result.IsSuccess = _adminUserRep.Add(adminUser) > 0;
            return Json(result);
        }

        [HttpPost]
        public ActionResult EditSave(AdminUserViewModel viewModel)
        {
            ResultModel result = new ResultModel();
            AdminUser adminUser = _adminUserRep.GetModelById(viewModel.Id);
            Mapper.Map(viewModel, adminUser);
            result.IsSuccess = _adminUserRep.Update(adminUser) > 0;
            return Json(result);
        }

        public ActionResult Del(int id)
        {
            ResultModel result = new ResultModel();
            result.IsSuccess = false;
            var adminUser = _adminUserRep.GetModelById(id);
            if (adminUser != null)
            {
                result.IsSuccess = _adminUserRep.Remove(adminUser) > 0;
            }

            return Json(result);
        }

        public ActionResult IsExistUserName(string userName)
        {
            return Json(!_adminUserRep.Any(q => q.UserName == userName));
        }

        public ActionResult ResetPwd(int id)
        {
            ResultModel result = new ResultModel();
            result.IsSuccess = _adminUserRep.ChangePwd(id, "111111");
            return Json(result);
        }
    }
}