using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Services.Group.Contracts.Group
{
    public class DtoChangeGroupNameRequest
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
    }
}
