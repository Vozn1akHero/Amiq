using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmicaPlus.Base
{
    [ApiController]
    public class AmicaPlusBaseController : ControllerBase
    {
        private ILogger<AmicaPlusBaseController> _logger;

        public AmicaPlusBaseController()
        {
        }
    }
}
