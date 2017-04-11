using System.Web.Mvc;

namespace NtAbcExam.Web.Areas.Admin.Models
{
    public class AdminUserViewModel
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string UserPwd { get; set; }

        public int DeptId { get; set; }

        public string DeptName { get; set; }

        public bool IsSa { get; set; }

        public SelectList DeptSelectList { get; set; }
    }
}