using Microsoft.AspNetCore.Mvc;

namespace Amiq.Services.User.Controllers
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
