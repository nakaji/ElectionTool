using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ElectionTool.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using WebGrease.Css.Extensions;

namespace ElectionTool.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new HomeIndexViewModel();
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            model.Users = manager.Users;
            model.TwitterWidgetId = ConfigurationManager.AppSettings["TwitterWidgetId"];

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}