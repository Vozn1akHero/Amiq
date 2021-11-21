using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Contracts.Activity
{
    public class DtoCreateGroupVisitation
    {
        public int GroupId { get; set; }
        public DateTime LastVisited { get; set; }
        public long VisitationTotalTime { get; set; }
    }
}
