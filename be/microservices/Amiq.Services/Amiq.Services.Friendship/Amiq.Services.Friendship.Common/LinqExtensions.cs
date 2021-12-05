using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Services.Friendship.Common
{
    public static class LinqExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int page, int count) where T : class
        {
            return query.Skip((page - 1) * count).Take(count);
        }
    }
}
