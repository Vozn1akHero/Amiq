using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Contracts.Activity
{
    public class DtoCreateActivityInBulk
    {
        public List<DtoCreateGroupVisitation> GroupVisitations { get; set; }
        public List<DtoCreateProfileVisitation> ProfileVisitations { get; set; }
    }
}
