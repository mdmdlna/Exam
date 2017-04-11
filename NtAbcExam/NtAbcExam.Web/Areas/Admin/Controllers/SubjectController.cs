using System.Collections.Generic;
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
    public class SubjectController : Controller
    {
        private readonly ExamSubjectRepository _subjectRep = new ExamSubjectRepository();

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
            exam_subject subject = _subjectRep.GetModelById(id);
            SubjectViewModel viewModel = new SubjectViewModel();
            Mapper.Map(subject, viewModel);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult GetList(ModelQuery modelQuery)
        {
            List<ModelFilter> filters = JsonConvert.DeserializeObject<List<ModelFilter>>(modelQuery.filters);
            modelQuery.filters = JsonConvert.SerializeObject(filters);

            int totalRow = 0;
            var subjectList = _subjectRep.Query(modelQuery, out totalRow);
            var subjectViewModels = Mapper.Map<List<exam_subject>, List<SubjectViewModel>>(subjectList);

            GridModel grid = new GridModel();
            grid.total = totalRow;
            grid.rows = subjectViewModels;
            return Content(JsonConvert.SerializeObject(grid));
        }

        [HttpPost]
        public ActionResult AddNew(SubjectViewModel viewModel)
        {
            ResultModel result = new ResultModel();
            exam_subject subject = new exam_subject();
            Mapper.Map(viewModel, subject);
            result.IsSuccess = _subjectRep.Add(subject) > 0;
            return Json(result);
        }

        [HttpPost]
        public ActionResult EditSave(SubjectViewModel viewModel)
        {
            ResultModel result = new ResultModel();
            exam_subject subject = _subjectRep.GetModelById(viewModel.Id);
            Mapper.Map(viewModel, subject);
            result.IsSuccess = _subjectRep.Update(subject) > 0;
            return Json(result);
        }

        public ActionResult Del(int id)
        {
            ResultModel result = new ResultModel();
            result.IsSuccess = false;
            var subject = _subjectRep.GetModelById(id);
            if (subject != null)
            {
                result.IsSuccess = _subjectRep.Remove(subject) > 0;
            }

            return Json(result);
        }

        public ActionResult IsExistSubjectName(int id, string subjectName)
        {
            return Json(!_subjectRep.Any(q => q.Id != id && q.SubjectName == subjectName));
        }
    }
}