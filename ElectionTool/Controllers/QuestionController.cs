﻿using System;
using System.Configuration;
using System.Web.Mvc;
using CoreTweet;
using ElectionTool.Helper;
using ElectionTool.Models;
using System.Threading.Tasks;
using ElectionTool.ViewModels;

namespace ElectionTool.Controllers
{
    public class QuestionController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        public async Task<ActionResult> Entry(string question)
        {
            var helper = new TwitterHelperForResident(this);
            if (helper.IsAuthorized)
            {
                // Twitterへの投稿
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

                var model = new HomeIndexViewModel {IsSucceeded = true};

                return View("../Home/Index", model);
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