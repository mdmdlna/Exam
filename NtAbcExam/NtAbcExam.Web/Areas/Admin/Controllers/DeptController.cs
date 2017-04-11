using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using AutoMapper;
using Newtonsoft.Json;
using NtAbcExam.Domain.Models;
using NtAbcExam.Domain.Repositories;
using NtAbcExam.FrameWork.Data;
using NtAbcExam.Web.Areas.Admin.Filters;
using NtAbcExam.Web.Areas.Admin.Models;
using NtAbcExam.Web.Models;

namespace NtAbcExam.Web.Areas.Admin.Controllers
{
    [AdminSeesion]
    public class DeptController : Controller
    {
        private readonly DepartmentRepository _deptRep = new DepartmentRepository();
        // GET: Admin/Dept
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            department dept = _deptRep.GetModelById(id);
            DeptViewModel viewModel = new DeptViewModel();
            Mapper.Map(dept, viewModel);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult GetList(ModelQuery modelQuery)
        {
            List<ModelFilter> filters = JsonConvert.DeserializeObject<List<ModelFilter>>(modelQuery.filters);
            modelQuery.filters = JsonConvert.SerializeObject(filters);

            int totalRow = 0;
            var deptList = _deptRep.Query(modelQuery, out totalRow);
            var deptViewModels = Mapper.Map<List<department>, List<DeptViewModel>>(deptList);

            GridModel grid = new GridModel();
            grid.total = totalRow;
            grid.rows = deptViewModels;
            return Content(JsonConvert.SerializeObject(grid));
        }

        [HttpPost]
        public ActionResult AddNew(DeptViewModel deptViewModel)
        {
            ResultModel result = new ResultModel();
            department dept = new department();
            Mapper.Map(deptViewModel, dept);
            result.IsSuccess = _deptRep.Add(dept) > 0;
            return Json(result);
        }

        [HttpPost]
        public ActionResult EditSave(DeptViewModel deptViewModel)
        {
            ResultModel result = new ResultModel();
            department dept = _deptRep.GetModelById(deptViewModel.Id);
            Mapper.Map(deptViewModel, dept);
            result.IsSuccess = _deptRep.Update(dept) > 0;
            return Json(result);
        }

        public ActionResult Del(int id)
        {
            ResultModel result = new ResultModel();
            result.IsSuccess = false;
            var dept = _deptRep.GetModelById(id);
            if (dept != null)
            {
                result.IsSuccess = _deptRep.Remove(dept) > 0;
            }

            return Json(result);
        }

        public ActionResult IsExistDeptName(int id, string deptName)
        {
            return Json(!_deptRep.Any(q => q.Id != id && q.DeptName == deptName));
        }
    }
}