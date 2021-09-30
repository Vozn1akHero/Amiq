using System;
using System.Collections.Generic;

#nullable disable

namespace Amiq.DataAccess.Models.Models
{
    public partial class UserDescriptionBlock
    {
        public Guid UserDescriptionBlockId { get; set; }
        public int UserId { get; set; }
        public Guid TextBlockId { get; set; }

        public virtual TextBlock TextBlock { get; set; }
        public virtual User User { get; set; }
    }
}
