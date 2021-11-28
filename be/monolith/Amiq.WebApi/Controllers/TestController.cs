using Amiq.WebApi.Base;
using Microsoft.AspNetCore.Mvc;

namespace Amiq.WebApi.Controllers
{
    public class TestController : AmiqBaseController
    {
        [HttpGet]
        public IActionResult DefGet()
        {
            return Ok(1);
        }
    }
}
