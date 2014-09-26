using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CoreTweet;

namespace ElectionTool.Helper
{
    public abstract class TwitterHelper
    {
        public Tokens Token { get; protected set; }

        public async Task<StatusResponse> StatusUpdateAsync(string message)
        {
            return await Token.Statuses.UpdateAsync(status => message);
        }
    }

    public class TwitterHelperForCandidate : TwitterHelper
    {
        public TwitterHelperForCandidate(Controller controller)
        {
            var consumerKey = ConfigurationManager.AppSettings["TwitterApiKeyForCandidate"];
            var consumerSecret = ConfigurationManager.AppSettings["TwitterApiKeySecretForCandidate"];
            var accessToken = controller.Session["AccessToken"].ToString();
            var accessTokenSecret = controller.Session["AccessTokenSecret"].ToString();

            Token = Tokens.Create(consumerKey, consumerSecret, accessToken, accessTokenSecret);
        }

        public async Task<StatusResponse> StatusUpdateAsync(string message, long replyStatusId)
        {
            return await Token.Statuses.UpdateAsync(status => message, in_reply_to_status_id => replyStatusId);
        }
    }

    public class TwitterHelperForResident : TwitterHelper
    {
        public TwitterHelperForResident(Controller controller)
        {
            var consumerKey = ConfigurationManager.AppSettings["TwitterApiKey"];
            var consumerSecret = ConfigurationManager.AppSettings["TwitterApiKeySecret"];
            var accessToken = controller.Session["AccessToken"].ToString();
            var accessTokenSecret = controller.Session["AccessTokenSecret"].ToString();

            Token = Tokens.Create(consumerKey, consumerSecret, accessToken, accessTokenSecret);
        }
    }
}