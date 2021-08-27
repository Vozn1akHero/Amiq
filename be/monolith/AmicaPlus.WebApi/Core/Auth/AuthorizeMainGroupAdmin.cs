using AmicaPlus.Contracts.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmicaPlus.WebApi.Core.Auth
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class AuthorizeMainGroupAdmin : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var jwt = context.HttpContext.Request.Cookies.Where(e => e.Key == "token");
            if (jwt == null)
                context.Result = new ForbidResult();

            //var user = (DtoJwtStoredUserInfo)context.HttpContext.Items["User"];
        }
    }
}
