using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Contracts.Group
{
    public class DtoGroupEvent
    {
        public Guid GroupEventId { get; set; }
        public int GroupId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string AvatarSrc { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public string Description { get; set; }
        public bool IsCancelled { get; set; }
        public bool IsHidden { get; set; }
        public List<DtoGroupEventParticipant> GroupEventParticipants { get; set; }
    }
}
