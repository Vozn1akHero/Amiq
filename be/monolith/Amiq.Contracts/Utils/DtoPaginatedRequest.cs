using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Contracts.Utils
{
    public class DtoPaginatedRequest
    {
        public int Page { get; set; }
        public int Count { get; set; }
    }
}
