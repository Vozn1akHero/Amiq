using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmicaPlus.Base
{
    class PaginatedRequest
    {
        public int PageIndex { get; set; }
        public int Count { get; set; }
        public string SortDirection { get; set; }
    }
}
