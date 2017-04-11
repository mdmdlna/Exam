using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NtAbcExam.Web.Models
{
    public class GridModel
    {
        /// <summary>
        /// 总页数
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// 记录
        /// </summary>

        public object rows { get; set; }
    }
}