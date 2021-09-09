using Amiq.Contracts.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Contracts.Post
{
    public class DtoGroupPostRequest : DtoPaginatedRequest
    {
        public int GroupId { get; set; }
    }
}
