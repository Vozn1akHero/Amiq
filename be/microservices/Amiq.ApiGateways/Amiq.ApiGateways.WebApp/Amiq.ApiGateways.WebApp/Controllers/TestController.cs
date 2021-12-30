using Microsoft.AspNetCore.Mvc;

namespace Amiq.ApiGateways.WebApp.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(1);
        }
    }
}
