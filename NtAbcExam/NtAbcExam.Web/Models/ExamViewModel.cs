using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NtAbcExam.Web.Models
{
    public class ExamViewModel
    {
        public int TestId { get; set; }

        public string SubjectName { get; set; }

        public int TestTime { get; set; }
    }
}