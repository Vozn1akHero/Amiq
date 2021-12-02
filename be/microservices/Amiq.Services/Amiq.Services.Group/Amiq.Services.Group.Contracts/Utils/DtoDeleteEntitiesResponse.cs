using System.Collections.Generic;

namespace Amiq.Services.Group.Contracts.Utils
{
    public class DtoDeleteEntitiesResponse
    {
        public bool IsBusinessException { get; set; }
        public string BusinessException { get; set; }
        public IEnumerable<object> Entities { get; set; }
    }
}
