using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NtAbcExam.Domain.Models;

namespace NtAbcExam.Web.Models
{
    public class QuestionViewModel
    {
        public int Id { get; set; }

        public int TestId { get; set; }

        public int Index { get; set; }

        public int Total { get; set; }
        
        public string Content { get; set; }

        public int Balance { get; set; }

        public QuestionType Type { get; set; }

        public Dictionary<string, string> Options { get; set; } = new Dictionary<string, string>();

        public List<string> Answers { get; set; } = new List<string>();

        public int SingleCount { get; set; }

        public int MultiCount { get; set; }

        public int JudgeCount { get; set; }
    }
}