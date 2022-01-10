using Amiq.Services.Group.Base;
using Microsoft.AspNetCore.Mvc;

namespace Amiq.Services.Group.Controllers
{
    public class TestController : AmiqGroupBaseController
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Environment.MachineName);
        }
    }
}
