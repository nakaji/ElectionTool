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
        public DbSet<Answer> Answers { get; set; }
    }

    public class Question
    {
        public int Id { get; set; }

        [Display(Name = "内容")]
        public string Text { get; set; }

        public long TweetId { get; set; }

        public string ScreenName { get; set; }

        public ICollection<Answer> Answers { get; set; } 
    }

    public class Answer
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        [Display(Name = "内容")]
        public string Text { get; set; }

        public long TweetId { get; set; }

        public Question Question { get; set; }
    }
}