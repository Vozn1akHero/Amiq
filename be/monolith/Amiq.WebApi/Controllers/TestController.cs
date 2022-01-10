using Amiq.WebApi.Base;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Amiq.WebApi.Controllers
{
    public class TestController : AmiqBaseController
    {
        [HttpGet]
        public IActionResult DefGet()
        {
            return Ok(Environment.MachineName);
        }
    }
}
