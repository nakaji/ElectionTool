using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectionTool.Models
{
    public class CandidateTimelineViewModel
    {
        public IEnumerable<Question> Questions { get; set; }
        public string UserId { get; set; }
    }
}