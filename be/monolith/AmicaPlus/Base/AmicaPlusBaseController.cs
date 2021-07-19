using AmicaPlus.Utils;
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
        private static SynchronizedCache _synchronizedCache = new SynchronizedCache();

        public static SynchronizedCache Cache { get => _synchronizedCache; }

        public AmicaPlusBaseController()
        {
        }
    }
}
