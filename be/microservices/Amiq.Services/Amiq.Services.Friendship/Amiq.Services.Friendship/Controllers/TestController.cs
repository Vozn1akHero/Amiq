using Microsoft.AspNetCore.Mvc;

namespace Amiq.Services.Friendship.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetTest()
        {
            return Ok(1);
        }
    }
}
