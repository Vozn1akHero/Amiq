using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Contracts.Group
{
    public class DtoGroupViewer
    {
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public EnGroupViewerRole GroupViewerRole { get; set; }
    }

    public enum EnGroupViewerRole { 
        Creator, Admin, Participant, Guest, Blocked
    }
}
