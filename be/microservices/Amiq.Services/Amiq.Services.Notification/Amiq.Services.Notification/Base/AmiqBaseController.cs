using Microsoft.AspNetCore.Mvc;

namespace Amiq.Services.Notification.Base
{
    [ApiController]
    [Route("api/[controller]")]
    public class AmiqBaseController : ControllerBase
    {
        public int JwtStoredUserId => int.Parse(HttpContext.Request.Headers["Amiq-UserId"]);
    }
}
