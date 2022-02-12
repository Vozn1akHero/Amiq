using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Services.Notification.Contracts.Activity
{
    public class DtoCreateProfileVisitation
    {
        public int VisitedUserId { get; set; }
        public DateTime LastVisited { get; set; }
        public long VisitationTotalTime { get; set; }
    }
}
