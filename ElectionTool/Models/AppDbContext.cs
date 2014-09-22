using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ElectionTool.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("DefaultConnection") { }

        public DbSet<Question> Questions { get; set; }
    }

    public class Question
    {
        public int Id { get; set; }

        [Display(Name = "内容")]
        public string Text { get; set; }

        public long TweetId { get; set; }
    }
}