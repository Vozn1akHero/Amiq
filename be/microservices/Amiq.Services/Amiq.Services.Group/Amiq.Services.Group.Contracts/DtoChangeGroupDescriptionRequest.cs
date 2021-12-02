using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Services.Group.Contracts
{
    public class DtoChangeGroupDescriptionRequest
    {
        public int GroupId { get; set; }
        public string Description { get; set; }
    }
}
