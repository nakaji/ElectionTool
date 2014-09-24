using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using CoreTweet;
using Microsoft.Owin.Security.Twitter.Messages;

namespace ElectionTool.Controllers
{
    public class AuthCallbackController : Controller
    {
        // GET: Auth
        public ActionResult Twitter(string oauth_token, string oauth_verifier)
        {
            var oAuthSession = Session["OAuthSession"] as OAuth.OAuthSession;
            var token = oAuthSession.GetTokens(oauth_verifier);
            Session["AccessToken"] = token.AccessToken;
            Session["AccessTokenSecret"] = token.AccessTokenSecret;

            var question = Session["Question"] as string;

            return Redirect("~/Question/Entry?question=" + question);
        }
    }
}