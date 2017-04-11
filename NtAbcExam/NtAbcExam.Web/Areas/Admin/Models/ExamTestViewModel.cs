using System;
using System.Web.Mvc;

namespace NtAbcExam.Web.Areas.Admin.Models
{
    public class ExamTestViewModel
    {
        public int Id { get; set; }

        public int TestId { get; set; }

        public int DeptId { get; set; }

        public string DeptName { get; set; }

        public int SubjectId { get; set; }

        public string SubjectName { get; set; }

        public int SingleCount { get; set; }

        public decimal SinglePer { get; set; }


        public int MultiCount { get; set; }

        public decimal MultiPer { get; set; }

        public int JudgeCount { get; set; }


        public decimal JudgePer { get; set; }


        public int TestTime { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string EmpIds { get; set; }
    }
}