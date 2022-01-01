using System;
using System.Collections.Generic;

namespace Amiq.DataAccessLayer.Models.Models
{
    public partial class TextBlock
    {
        public TextBlock()
        {
            GroupDescriptionBlocks = new HashSet<GroupDescriptionBlock>();
            UserDescriptionBlocks = new HashSet<UserDescriptionBlock>();
        }

        public Guid TextBlockId { get; set; }
        public string Header { get; set; } = null!;
        public string? Content { get; set; }

        public virtual ICollection<GroupDescriptionBlock> GroupDescriptionBlocks { get; set; }
        public virtual ICollection<UserDescriptionBlock> UserDescriptionBlocks { get; set; }
    }
}
