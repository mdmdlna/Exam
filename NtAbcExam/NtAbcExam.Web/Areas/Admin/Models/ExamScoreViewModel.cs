using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NtAbcExam.Web.Areas.Admin.Models
{
    public class ExamScoreViewModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public int DeptId { get; set; }

        public string DeptName { get; set; }

        public int SubjectId { get; set; }


        public string SubjectName { get; set; }

        public int TestId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int Score { get; set; }
    }
}
