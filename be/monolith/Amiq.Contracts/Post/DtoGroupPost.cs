using System.Collections.Generic;

namespace Amiq.Contracts.Post
{
    public class DtoGroupPost : DtoPost
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public bool VisibleAsCreatedByAdmin { get; set; }
        public List<DtoGroupPostComment> Comments { get; set; }
    }
}
