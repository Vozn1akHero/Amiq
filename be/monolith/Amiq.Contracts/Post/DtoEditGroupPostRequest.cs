using Amiq.Contracts.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Contracts.Post
{
    public class DtoEditGroupPostRequest 
    {
        public Guid GroupPostId { get; set; }
        public int EditedBy { get; set; }
        public string Text { get; set; }
    }
}
