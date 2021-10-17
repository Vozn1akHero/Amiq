using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Contracts.Group
{
    public class DtoGroupUserParams
    {
        public int GroupId { get; set; }
        public bool IsHidden { get; set; }
    }
}
