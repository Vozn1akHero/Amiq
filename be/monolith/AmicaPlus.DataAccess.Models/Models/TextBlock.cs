using System;
using System.Collections.Generic;

#nullable disable

namespace AmicaPlus.DataAccess.Models.Models
{
    public partial class TextBlock
    {
        public TextBlock()
        {
            GroupDescriptionBlocks = new HashSet<GroupDescriptionBlock>();
        }

        public Guid TextBlockId { get; set; }
        public string Header { get; set; }
        public string Content { get; set; }

        public virtual ICollection<GroupDescriptionBlock> GroupDescriptionBlocks { get; set; }
    }
}
