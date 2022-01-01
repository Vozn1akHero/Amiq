using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Contracts.Activity
{
    public class DtoCreateActivityInBulk
    {
        [Required]
        public List<DtoCreateGroupVisitation> GroupVisitations { get; set; }

        [Required]
        public List<DtoCreateProfileVisitation> UserProfileVisitations { get; set; }
    }
}
