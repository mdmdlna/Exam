using System.Web.Mvc;

namespace NtAbcExam.Web.Areas.Admin.Models
{
    public class EmpViewModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public string Pwd { get; set; }

        public int DeptId { get; set; }

        public string DeptName { get; set; }

        public string Office { get; set; }

        public string Duties { get; set; }

        public string Rank { get; set; }

        public string Post { get; set; }

        public string Spower { get; set; }

        public SelectList DeptSelectList { get; set; }
    }
}