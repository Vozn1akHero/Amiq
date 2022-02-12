using System.Collections.Generic;

namespace Amiq.Services.Common.Contracts
{
    public class DtoListResponseOf<T> where T : class
    {
        public List<T> Entities { get; set; }
        public int Length { get; set; }
    }
}
