using Amiq.Business.Chat;
using Amiq.Contracts.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Amiq.WebApi.Core.Auth
{
    /// <summary>
    /// Atrybut używany do autoryzacji użytkownika próbującego uzyskać dostęp do czatu
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class AuthorizeChatInterlocutor : Attribute, IAuthorizationFilter
    {
        private BsChat _bsChat = new BsChat();

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var query = context.HttpContext.Request.Query;
            var user = (DtoJwtStoredUserInfo)context.HttpContext.Items["user"];

            try
            {
                /*if (!string.IsNullOrEmpty(query["RequestIssuerId"]))
                {
                    //int.TryParse(query["RequestIssuerId"], out int requestIssuerId);
                    
                } else {
                    context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                }*/
                int requestIssuerId = user.UserId;
                Guid.TryParse(query["ChatId"], out Guid chatId);
                bool res = _bsChat.IsUserChatParticipant(requestIssuerId, chatId);
                if (!res)
                    context.Result = new ForbidResult();
            } catch 
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }
        }
    }
}
