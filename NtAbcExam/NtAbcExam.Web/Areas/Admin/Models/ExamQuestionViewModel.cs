using System.Web.Mvc;

namespace NtAbcExam.Web.Areas.Admin.Models
{
    public class ExamQuestionViewModel
    {
        public int Id { get; set; }

        public string Question { get; set; }

        public string Type { get; set; }


        public string Text1 { get; set; }
        public string Text2 { get; set; }
        public string Text3 { get; set; }
        public string Text4 { get; set; }
        public string Text5 { get; set; }
        public string Text6 { get; set; }
        public string Answer { get; set; }



    }
}