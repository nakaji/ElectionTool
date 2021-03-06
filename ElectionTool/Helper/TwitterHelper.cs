﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CoreTweet;
using ElectionTool.Models;
using Microsoft.AspNet.Identity;

namespace ElectionTool.Helper
{
    public abstract class TwitterHelper
    {
        protected readonly string Hashtag = ConfigurationManager.AppSettings["Hashtag"];

        public Tokens Token { get; protected set; }

        public async Task<StatusResponse> StatusUpdateAsync(string message)
        {
            var postMassage = string.Format("{0} #{1}", message, Hashtag);

            return await Token.Statuses.UpdateAsync(status => postMassage);
        }
    }

    public class TwitterHelperForCandidate : TwitterHelper
    {
        public TwitterHelperForCandidate(Controller controller)
        {
            var consumerKey = ConfigurationManager.AppSettings["TwitterApiKeyForCandidate"];
            var consumerSecret = ConfigurationManager.AppSettings["TwitterApiKeySecretForCandidate"];

            using (var db = new ApplicationDbContext())
            {
                var userId = controller.User.Identity.GetUserId();
                var user = db.Users.First(x => x.Id == userId);
                var accessToken = user.AccessToken;
                var accessTokenSecret = user.AccessTokenSecret;
                Token = Tokens.Create(consumerKey, consumerSecret, accessToken, accessTokenSecret);
            }
        }

        public async Task<StatusResponse> StatusUpdateAsync(string message, long replyStatusId, string screenName)
        {
            var postMassage = string.Format(". @{0} {1} #{2}", screenName, message, Hashtag);

            return await Token.Statuses.UpdateAsync(status => postMassage, in_reply_to_status_id => replyStatusId);
        }
    }

    public class TwitterHelperForResident : TwitterHelper
    {
        public bool IsAuthorized { get; private set; }

        public TwitterHelperForResident(Controller controller)
        {
            if (controller.Session["AccessToken"] == null)
            {
                IsAuthorized = false;
                return;
            }
            var consumerKey = ConfigurationManager.AppSettings["TwitterApiKey"];
            var consumerSecret = ConfigurationManager.AppSettings["TwitterApiKeySecret"];
            var accessToken = controller.Session["AccessToken"].ToString();
            var accessTokenSecret = controller.Session["AccessTokenSecret"].ToString();

            Token = Tokens.Create(consumerKey, consumerSecret, accessToken, accessTokenSecret);
            IsAuthorized = true;
        }
    }
}