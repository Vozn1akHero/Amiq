using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Contracts.Post
{
    public class DtoCreateGroupPostComment : DtoCreatePostComment
    {
        public int GroupId { get; set; }
        public string AuthorVisibilityType { get; set; }
    }
}
