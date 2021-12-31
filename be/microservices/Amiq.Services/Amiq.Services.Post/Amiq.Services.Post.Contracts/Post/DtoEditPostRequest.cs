using Amiq.Contracts.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Services.Post.Contracts.Post
{
    public class DtoEditPostRequest
    {
        public Guid PostId { get; set; }
        public int EditedBy { get; set; }
        public string Text { get; set; }
    }
}
