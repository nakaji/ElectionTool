using System;
using System.Collections.Generic;
using ElectionTool.Models;

namespace ElectionTool.ViewModels
{
    public class AnswerIndexViewModel
    {
        public AnswerIndexViewModel()
        {
            Questions = new List<Tuple<Question, Answer>>();
        }
        //public IEnumerable<Question> Questions { get; set; }
        public List<Tuple<Question, Answer>> Questions { get; set; }
    }

    public class AnswerEntryViewModel
    {
        public Question Question { get; set; }

        public string Answer { get; set; }
    }
}