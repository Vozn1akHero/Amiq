using System.Collections.Generic;

namespace Amiq.Contracts
{
    public class DtoDeleteEntitiesResponse
    {
        public bool Result { get; set; }
        public string Message { get; set; }
        public IEnumerable<object> Entities { get; set; }
    }
}
