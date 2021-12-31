using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Services.Post.Contracts.Post
{
    public class DtoEditUserPostRequest
    {
        public int UserId { get; set; }
        public Guid PostId { get; set; }
        public string TextContent { get; set; }
    }
}
