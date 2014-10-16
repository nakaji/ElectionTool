using System.Collections.Generic;
using ElectionTool.Models;

namespace ElectionTool.ViewModels
{
    public class CandidateTimelineViewModel
    {
        public IEnumerable<Question> Questions { get; set; }
        public string UserId { get; set; }
    }
}