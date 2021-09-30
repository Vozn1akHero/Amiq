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

        public DtoFriendRequest CreateFriendRequest(int issuerId, int receiverId)
        {
            var record = new FriendRequest { IssuerId = issuerId, ReceiverId = receiverId };
            _amiqContext.FriendRequests.Add(record);
            _amiqContext.SaveChanges();
            return new DtoFriendRequest { FriendRequestId = record.FriendRequestId,
                IssuerId = issuerId, 
                ReceiverId = receiverId };
        }

        public async Task DeleteFriendRequestAsync(int issuerId, int receiverId)
        {
            var friendRequest = _amiqContext.FriendRequests.SingleOrDefault(e => e.IssuerId == issuerId && e.ReceiverId == receiverId);
            if (friendRequest != null)
            {
                _amiqContext.FriendRequests.Remove(friendRequest);
                await _amiqContext.SaveChangesAsync();
            }
        }
    }
}
