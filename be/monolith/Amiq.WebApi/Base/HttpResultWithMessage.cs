using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Amiq.WebApi.Base
{
    public class StatusCodeResultWithMessage
    {
        public StatusCodeResult StatusCodeResult { get; set; }
        public string Message { get; set; }
    }
}
