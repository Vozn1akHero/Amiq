using Amiq.Contracts.Friendship;
using Amiq.DataAccess.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.DataAccess.Friendship
{
    public class DaFriendship
    {
        private AmiqContext _amiqContext = new AmiqContext();

        public async Task<IEnumerable<DtoFriend>> GetUserFriendListAsync(DtoFriendListRequest request)
        {
            return await (from fr in _amiqContext.Friendships.AsNoTracking()
                          join u in _amiqContext.Users.AsNoTracking()
                          on new { FirstUserId = fr.FirstUserId, SecondUserId = fr.SecondUserId } equals new { FirstUserId = u.UserId, SecondUserId = u.UserId }
                          where fr.FirstUserId == request.IssuerId || fr.SecondUserId == request.IssuerId
                          select new DtoFriend { 
                            UserId = fr.FirstUserId != request.IssuerId ? fr.FirstUserId : fr.SecondUserId,
                            Name = u.Name,
                            Surname = u.Surname,
                          })
                          .Skip(request.PageIndex * request.Count)
                          .Take(request.Count)
                          .ToListAsync();
        }
    }
}
