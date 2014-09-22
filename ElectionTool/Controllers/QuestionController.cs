using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoreTweet;
using ElectionTool.Models;

namespace ElectionTool.Controllers
{
    public class QuestionController : Controller
    {
        public ActionResult Entry(string question)
        {
            var token = Session["AccessToken"] as Tokens;
            if (token != null)
            {
                // todo:Twitterへの投稿
                token.Statuses.Update(status => question);
                // todo:データベースへの登録


                return View();
            }

            // todo:Twitterへの認証チェック
            var consumerKey = ConfigurationManager.AppSettings["TwitterApiKey"];
            var consumerSecret = ConfigurationManager.AppSettings["TwitterApiKeySecret"];

            var oAuthSession = OAuth.Authorize(consumerKey, consumerSecret, "http://mydomain.com:63543/AuthCallback/Twitter");

            Session["OAuthSession"] = oAuthSession;
            Session["Question"] = question;
            
            return Redirect(oAuthSession.AuthorizeUri.OriginalString);
        }
    }
}