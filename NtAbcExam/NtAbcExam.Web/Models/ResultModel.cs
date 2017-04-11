using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NtAbcExam.Web.Models
{
    public class ResultModel
    {
        public bool IsSuccess { get; set; }

        public string Msg { get; set; }

        public object Data { get; set; }
    }
}