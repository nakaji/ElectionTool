using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ElectionTool.Models;

namespace ElectionTool.Controllers
{
    public class QuestionController : Controller
    {
        [HttpPost]
        public ActionResult Entry(HomeIndexViewModel model)
        {
            // todo:Twitterへの認証チェック

            // todo:データベースへの登録

            // todo:Twitterへの投稿

            return View();
        }
    }
}