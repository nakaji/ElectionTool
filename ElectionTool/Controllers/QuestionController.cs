using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoreTweet;
using ElectionTool.Helper;
using ElectionTool.Models;
using System.Threading.Tasks;

namespace ElectionTool.Controllers
{
    public class QuestionController : Controller
    {
        public async Task<ActionResult> Entry(string question)
        {
            if (Session["AccessToken"] != null)
            {
                var accessToken = Session["AccessToken"].ToString();
                var accessTokenSecret = Session["AccessTokenSecret"].ToString();
                // Twitterへの投稿
                var helper = new TwitterHelperForResident(accessToken, accessTokenSecret);
                var response = await helper.StatusUpdateAsync(question);
                
                // データベースへの登録
                using (var db = new AppDbContext())
                {
                    db.Questions.Add(new Question()
                    {
                        Text = question,
                        TweetId = response.Id
                    });

                    await db.SaveChangesAsync();
                }

                return View();
            }

            // Twitterへの認証
            var consumerKey = ConfigurationManager.AppSettings["TwitterApiKey"];
            var consumerSecret = ConfigurationManager.AppSettings["TwitterApiKeySecret"];

            var oAuthSession = OAuth.Authorize(consumerKey, consumerSecret, "http://mydomain.com:63543/AuthCallback/Twitter");

            Session["OAuthSession"] = oAuthSession;
            Session["Question"] = question;

            return Redirect(oAuthSession.AuthorizeUri.OriginalString);
        }
    }
}