using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using ElectionTool.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ElectionTool.ViewModels
{
    public class HomeIndexViewModel
    {
        [MaxLength(100)]
        [Display(Name = "質問")]
        public string Question { get; set; }

        public List<CandidateInfo> CandidateInfos { get; private set; }

        public string TwitterWidgetId { get; private set; }

        public bool IsSucceeded { get; set; }

        public HomeIndexViewModel()
        {
            CandidateInfos = new List<CandidateInfo>();
            using (var db = new ApplicationDbContext())
            {
                var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
                var answers = db.Answers.GroupBy(x => x.UserId).ToList();
                foreach (var user in manager.Users)
                {
                    var replies = answers.FirstOrDefault(x => x.Key == user.Id);
                    CandidateInfos.Add(new CandidateInfo()
                    {
                        User = user,
                        ReplyCount = replies == null ? 0 : replies.Count()
                    });
                }
            }
            TwitterWidgetId = ConfigurationManager.AppSettings["TwitterWidgetId"];
        }

        public class CandidateInfo
        {
            public ApplicationUser User { get; set; }
            public int ReplyCount { get; set; }
        }
    }
}