using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ElectionTool.Models;
using ElectionTool.ViewModels;
using Microsoft.AspNet.Identity;

namespace ElectionTool.Controllers
{
    public class CandidateController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Timeline
        public ActionResult Timeline(string userId)
        {
            if (userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = new CandidateTimelineViewModel
            {
                UserId = userId,
                Questions = _db.Questions.Include("Answers").Where(x => x.Answers.Any(a => a.UserId == userId))
            };

            return PartialView(model);
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