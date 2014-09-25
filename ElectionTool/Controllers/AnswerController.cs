using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ElectionTool.Models;

namespace ElectionTool.Controllers
{
    public class AnswerController : Controller
    {
        private AppDbContext _db = new AppDbContext();

        // GET: Answer
        public ActionResult Index()
        {
            var model = new AnswerIndexViewModel();
            model.Questions = _db.Questions;

            return View(model);
        }
    }
}