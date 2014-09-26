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
            var accessToken = controller.Session["AccessToken"].ToString();
            var accessTokenSecret = controller.Session["AccessTokenSecret"].ToString();

            Token = Tokens.Create(consumerKey, consumerSecret, accessToken, accessTokenSecret);
        }

        public async Task<StatusResponse> StatusUpdateAsync(string message, long replyStatusId)
        {
            var postMassage = string.Format("{0} #{1}", message, Hashtag);

            return await Token.Statuses.UpdateAsync(status => postMassage, in_reply_to_status_id => replyStatusId);
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