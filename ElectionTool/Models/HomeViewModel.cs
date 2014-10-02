using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElectionTool.Models
{
    public class HomeIndexViewModel
    {
        [MaxLength(100)]
        [Display(Name="質問")]
        public string Question { get; set; }

        public IEnumerable<ApplicationUser> Users { get; set; }
    }
}