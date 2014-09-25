using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ElectionTool.Helper;
using ElectionTool.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity.Core;

namespace ElectionTool.Controllers
{
    public class AnswerController : Controller
    {
        private AppDbContext _db = new AppDbContext();

        // GET: Answer
        public ActionResult Index()
        {
            var model = new AnswerIndexViewModel();
            model.Questions = _db.Questions.Include("Answers");

            return View(model);
        }

        [HttpGet]
        public ActionResult Entry(int id)
        {
            var model = new AnswerEntryViewModel();
            model.Question = _db.Questions.Find(id);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Entry(AnswerEntryViewModel model)
        {
            var accessToken = Session["AccessToken"].ToString();
            var accessTokenSecret = Session["AccessTokenSecret"].ToString();
            // Twitterへの投稿
            var helper = new TwitterHelperForCandidate(accessToken, accessTokenSecret);
            var response = await helper.StatusUpdateAsync(model.Answer);
            
            var answer = new Answer()
            {
                UserId = User.Identity.GetUserId(),
                Text = model.Answer,
                TweetId = response.Id,
                Question = _db.Questions.Find(model.Question.Id)
            };

            _db.Answers.Add(answer);
            await _db.SaveChangesAsync();

            return View(model);
        }

    }
}