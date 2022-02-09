using System.Collections.Generic;

namespace Amiq.Services.Notification.Contracts.Utils
{
    public class DtoListResponseOf<T> where T : class
    {
        public List<T> Entities { get; set; }
        public int Length { get; set; }
    }
}
