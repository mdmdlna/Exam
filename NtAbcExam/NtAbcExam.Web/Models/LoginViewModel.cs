using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NtAbcExam.Web.Models
{
    public class LoginViewModel
    {
        [Display(Name = "账号")]
        [Required]
        [StringLength(20)]
        public string UserId { get; set; }

        [Display(Name = "密码")]
        [Required]
        [StringLength(32)]
        public string UserPwd { get; set; }
    }
}