using Amiq.Services.Base.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Amiq.Services.Group.Controllers
{
    public class TestController : AmiqBaseController
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Environment.MachineName);
        }
    }
}
