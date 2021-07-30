using System;
using System.Collections.Generic;

#nullable disable

namespace AmicaPlus.DataAccess.Models
{
    public partial class UserPost
    {
        public Guid UserPostId { get; set; }
        public Guid PostId { get; set; }

        public virtual Post UserPostNavigation { get; set; }
    }
}
