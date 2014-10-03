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
        private AppDbContext _db = new AppDbContext();

        public async Task<ActionResult> Entry(string question)
        {
            if (Session["AccessToken"] != null)
            {
                // Twitterへの投稿
                var helper = new TwitterHelperForResident(this);
                var response = await helper.StatusUpdateAsync(question);

                // データベースへの登録
                _db.Questions.Add(new Question()
                {
                    Text = question,
                    TweetId = response.Id,
                    ScreenName = response.User.ScreenName,
                    IconUri = response.User.ProfileImageUrl.AbsoluteUri,
                });

                await _db.SaveChangesAsync();

                return View();
            }

            // Twitterへの認証
            var consumerKey = ConfigurationManager.AppSettings["TwitterApiKey"];
            var consumerSecret = ConfigurationManager.AppSettings["TwitterApiKeySecret"];
            var siteUrl = Request.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.UriEscaped);

            var oAuthSession = OAuth.Authorize(consumerKey, consumerSecret, siteUrl + "/AuthCallback/Twitter");

            Session["OAuthSession"] = oAuthSession;
            Session["Question"] = question;

            return Redirect(oAuthSession.AuthorizeUri.OriginalString);
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