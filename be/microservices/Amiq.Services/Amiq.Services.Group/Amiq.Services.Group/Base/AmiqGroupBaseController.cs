using AutoMapper.Internal;
using Microsoft.AspNetCore.Mvc;

namespace Amiq.Services.Group.Base
{
    public class AmiqGroupBaseController : ControllerBase
    {
        public int UserId => int.Parse(HttpContext.Request.Headers["Amiq-UserId"]);

        public AmiqGroupBaseController(IHttpContextAccessor httpContextAccessor)
        {
        }
    }
}
