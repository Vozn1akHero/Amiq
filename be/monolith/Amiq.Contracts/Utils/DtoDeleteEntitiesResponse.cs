using System.Collections.Generic;

namespace Amiq.Contracts.Utils
{
    public class DtoDeleteEntitiesResponse
    {
        public bool IsBusinessException { get; set; }
        public string BusinessException { get; set; }
        public IEnumerable<object> Entities { get; set; }
    }
}
