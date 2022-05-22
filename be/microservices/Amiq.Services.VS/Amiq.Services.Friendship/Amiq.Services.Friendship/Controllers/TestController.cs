using Amiq.Services.Base.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Amiq.Services.Friendship.Controllers
{
    public class TestController : AmiqBaseController
    {
        [HttpGet]
        public IActionResult GetTest()
        {
            return Ok(1);
        }
    }
}
