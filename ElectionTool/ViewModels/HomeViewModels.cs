using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using ElectionTool.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ElectionTool.ViewModels
{
    public class HomeIndexViewModel
    {
        [MaxLength(100)]
        [Display(Name="質問")]
        public string Question { get; set; }

        public IEnumerable<ApplicationUser> Users { get; private set; }

        public string TwitterWidgetId { get; private set; }

        public bool IsSucceeded { get; set; }

        public HomeIndexViewModel()
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            Users = manager.Users;
            TwitterWidgetId = ConfigurationManager.AppSettings["TwitterWidgetId"];
        }
    }
}