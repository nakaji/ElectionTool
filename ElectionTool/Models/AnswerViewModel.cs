using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectionTool.Models
{
    public class AnswerIndexViewModel
    {
        public IEnumerable<Question> Questions { get; set; }
    }

    public class AnswerEntryViewModel
    {
        public Question Question { get; set; }

        public string Answer { get; set; }
    }
}