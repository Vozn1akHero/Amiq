using Amiq.Common.DbOperation;
using Amiq.Contracts;
using Amiq.Contracts.Friendship;
using Amiq.Contracts.Utils;
using Amiq.DataAccessLayer.Models.Models;
using Amiq.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.DataAccessLayer.Friendship
{
    public class DaoFriendship
    {
        private AmiqContext _amiqContext = new AmiqContext();

        public async Task<DtoListResponseOf<DtoFriend>> GetUserFriendListAsync(DtoGetFriendListRequest request)
        {
            DtoListResponseOf<DtoFriend> result = new();
            result.Length = await _amiqContext.Friendships.AsNoTracking()
                .Where(fr => fr.FirstUserId == request.IssuerId || fr.SecondUserId == request.IssuerId)
                .CountAsync();
            result.Entities = await (from fr in _amiqContext.Friendships.AsNoTracking()
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
            return result;
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

        public DbOperationResult RemoveFriend(int userId, int friendId)
        {
            DbOperationResult dbOperationResult = new();
            var entity = _amiqContext.Friendships.Single(e => (e.FirstUserId == userId && e.SecondUserId == friendId)
                || e.FirstUserId == friendId && e.SecondUserId == userId);
            _amiqContext.Friendships.Remove(entity);
            _amiqContext.SaveChangesAsync();
            dbOperationResult.Success = true;
            return dbOperationResult;
        }

        /*public async Task<DtoFriendSearchResult> SearchAsync(int issuerId, DtoPaginatedRequest paginatedRequest, string text)
        {
            DtoFriendSearchResult result = new();
            result.FoundsFriends = await SearchForUserFriendsAsync(issuerId, paginatedRequest, text);
            return result;
        }*/
    }
}
