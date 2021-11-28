using System;
using System.Collections.Generic;

namespace Amiq.DataAccessLayer.Models.Models
{
    public partial class GroupDescriptionBlock
    {
        public Guid GroupDescriptionBlockId { get; set; }
        public int GroupId { get; set; }
        public Guid TextBlockId { get; set; }

        public virtual Group Group { get; set; }
        public virtual TextBlock TextBlock { get; set; }
    }
}
