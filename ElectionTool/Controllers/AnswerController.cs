using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ElectionTool.Helper;
using ElectionTool.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity.Core;
using WebGrease.Css.Extensions;

namespace ElectionTool.Controllers
{
    [Authorize]
    public class AnswerController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Answer
        public ActionResult Index()
        {
            var model = new AnswerIndexViewModel();

            var userId = (User.Identity.GetUserId() ?? "");
            _db.Questions.Include("Answers").ForEach(q =>
            {
                var latestAnswer = q.Answers.OrderByDescending(x => x.Id).FirstOrDefault(x => x.UserId == userId);
                model.Questions.Add(new Tuple<Question, Answer>(q, latestAnswer));
            });

            return View(model);
        }

        [HttpGet]
        public ActionResult Entry(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var question = _db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }

            var model = new AnswerEntryViewModel();
            model.Question = question;

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Entry(AnswerEntryViewModel model)
        {
            var question = _db.Questions.Find(model.Question.Id);

            // Twitterへの投稿
            var helper = new TwitterHelperForCandidate(this);
            var response = await helper.StatusUpdateAsync(model.Answer, question.TweetId, question.ScreenName);

            var answer = new Answer()
            {
                UserId = User.Identity.GetUserId(),
                Text = model.Answer,
                TweetId = response.Id,
                ScreenName = response.User.ScreenName,
                IconUri = response.User.ProfileImageUrl.AbsoluteUri,
                Question = question
            };

            _db.Answers.Add(answer);
            await _db.SaveChangesAsync();

            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}