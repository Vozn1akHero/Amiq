using System;
using System.Collections.Generic;

namespace Amiq.Services.User.DataAccessLayer.Models
{
    public partial class UserDescriptionBlock
    {
        public Guid UserDescriptionBlockId { get; set; }
        public int UserId { get; set; }
        public Guid TextBlockId { get; set; }

        public virtual TextBlock TextBlock { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
