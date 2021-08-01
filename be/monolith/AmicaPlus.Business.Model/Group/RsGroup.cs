using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmicaPlus.ResultSets.Group
{
    public class RsGroup
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string AvatarSrc { get; set; }
        public string Description { get; set; }
        public List<RsGroupParticipant> Participants { get; set; }
    }

    //public record RsGroup(int GroupId, string Name, string AvatarSrc, string Description);
}
