using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ElectionTool.Models;

namespace ElectionTool.ViewModels
{
    public class HomeIndexViewModel
    {
        [MaxLength(100)]
        [Display(Name="質問")]
        public string Question { get; set; }

        public IEnumerable<ApplicationUser> Users { get; set; }

        public string TwitterWidgetId { get; set; }
    }
}