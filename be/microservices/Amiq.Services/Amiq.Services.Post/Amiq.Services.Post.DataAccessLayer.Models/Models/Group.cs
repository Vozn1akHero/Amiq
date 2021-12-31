using System;
using System.Collections.Generic;

namespace Amiq.Services.Post.DataAccessLayer.Models.Models
{
    public partial class Group
    {
        public int GroupId { get; set; }
        public string Name { get; set; } = null!;
        public string AvatarSrc { get; set; } = null!;
    }
}
