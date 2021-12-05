using Amiq.Contracts.Core;
using System.Collections.Generic;

namespace Amiq.Contracts.Group
{
    public class DtoEditGroupData
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string AvatarSrc { get; set; }
        public string Description { get; set; }
        public List<DtoDescriptionBlock> DescriptionBlocks { get; set; }
    }
}
