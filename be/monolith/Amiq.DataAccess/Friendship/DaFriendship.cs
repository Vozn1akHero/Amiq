using Amiq.Contracts.Friendship;
using Amiq.Contracts.Utils;
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
                          join u1 in _amiqContext.Users.AsNoTracking()
                          on fr.FirstUserId equals u1.UserId
                          join u2 in _amiqContext.Users.AsNoTracking()
                          on fr.SecondUserId equals u2.UserId
                          where fr.FirstUserId == request.IssuerId || fr.SecondUserId == request.IssuerId
                          select new DtoFriend { 
                            UserId = fr.FirstUserId != request.IssuerId ? fr.FirstUserId : fr.SecondUserId,
                            Name = u1.UserId == request.IssuerId ? u2.Name : u1.Name,
                            Surname = u1.UserId == request.IssuerId ? u2.Surname : u1.Surname,
                            AvatarPath = u1.UserId == request.IssuerId ? u2.AvatarPath : u1.AvatarPath
                          })
                          .Skip((request.Page-1) * request.Count)
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
            var friendRequest = _amiqContext
                .FriendRequests
                .SingleOrDefault(e => e.IssuerId == issuerId && e.ReceiverId == receiverId);
            if (friendRequest != null)
            {
                _amiqContext.FriendRequests.Remove(friendRequest);
                await _amiqContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<DtoFriend>> SearchForUserFriendsAsync(int issuerId, DtoPaginatedRequest request, string searchText)
        {
            return await (from fr in _amiqContext.Friendships.AsNoTracking()
                          join u1 in _amiqContext.Users.AsNoTracking()
                          on fr.FirstUserId equals u1.UserId
                          join u2 in _amiqContext.Users.AsNoTracking()
                          on fr.SecondUserId equals u2.UserId
                          where ((fr.FirstUserId == issuerId || fr.SecondUserId == issuerId) 
                            && (u1.UserId == issuerId ? (u2.Name + " " + u2.Surname).ToUpper().StartsWith(searchText.ToUpper()) 
                            : (u1.Name + " " + u1.Surname).ToUpper().StartsWith(searchText.ToUpper())))
                          select new DtoFriend
                          {
                              UserId = fr.FirstUserId != issuerId ? fr.FirstUserId : fr.SecondUserId,
                              Name = u1.UserId == issuerId ? u2.Name : u1.Name,
                              Surname = u1.UserId == issuerId ? u2.Surname : u1.Surname,
                              AvatarPath = u1.UserId == issuerId ? u2.AvatarPath : u1.AvatarPath
                          })
                          .Skip((request.Page - 1) * request.Count)
                          .Take(request.Count)
                          .ToListAsync();
        }


        /*public async Task<DtoFriendSearchResult> SearchAsync(int issuerId, DtoPaginatedRequest paginatedRequest, string text)
        {
            DtoFriendSearchResult result = new();
            result.FoundsFriends = await SearchForUserFriendsAsync(issuerId, paginatedRequest, text);
            return result;
        }*/
    }
}
