using Amiq.Services.Post.Contracts.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Services.Post.Contracts.Post
{
    public class DtoGroupPostRequest : DtoPaginatedRequest
    {
        public int GroupId { get; set; }
    }
}
