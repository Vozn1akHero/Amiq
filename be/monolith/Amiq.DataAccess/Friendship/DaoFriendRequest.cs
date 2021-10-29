using Amiq.Common.DbOperation;
using Amiq.Common.Enums;
using Amiq.Contracts;
using Amiq.Contracts.Friendship;
using Amiq.DataAccess.Models.Models;
using Amiq.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.DataAccess.Friendship
{
    public class DaoFriendRequest
    {
        private AmiqContext _amiqContext = new AmiqContext();

        public async Task<IEnumerable<DtoFriendRequest>> GetFriendRequestsAsync(int userId, FriendRequestType friendRequestType)
        {
            Expression<Func<FriendRequest, bool>> whereBody = null;
            if (friendRequestType == FriendRequestType.Creator)
                whereBody = e => e.IssuerId == userId;
            else if(friendRequestType == FriendRequestType.Receiver)
                whereBody = e=>e.ReceiverId == userId;
            IQueryable query = _amiqContext.FriendRequests.Where(whereBody);
            var result = await APAutoMapper.Instance.ProjectTo<DtoFriendRequest>(query).ToListAsync();
            return result;
        }

        public async Task<DbOperationResult> AcceptFriendRequestAsync(Guid friendRequestId)
        {
            DbOperationResult dbOperationResult = new();
            var entity = _amiqContext.FriendRequests.Single(e=>e.FriendRequestId==friendRequestId);
            _amiqContext.FriendRequests.Remove(entity);
            _amiqContext.Friendships.Add(new Models.Models.Friendship { 
                FirstUserId = entity.ReceiverId,
                SecondUserId = entity.IssuerId
            });
            await _amiqContext.SaveChangesAsync();
            dbOperationResult.Success = true;
            return dbOperationResult;
        }

        public async Task<DbOperationResult> RejectFriendRequestAsync(Guid friendRequestId)
        {
            DbOperationResult dbOperationResult = new();
            var entity = _amiqContext.FriendRequests.Single(e => e.FriendRequestId == friendRequestId);
            _amiqContext.FriendRequests.Remove(entity);
            await _amiqContext.SaveChangesAsync();
            dbOperationResult.Success = true;
            return dbOperationResult;
        }

        public async Task<DbOperationResult> CancelFriendRequestAsync(Guid friendRequestId)
        {
            DbOperationResult dbOperationResult = new();
            var entity = _amiqContext.FriendRequests.Single(e => e.FriendRequestId == friendRequestId);
            _amiqContext.FriendRequests.Remove(entity);
            await _amiqContext.SaveChangesAsync();
            dbOperationResult.Success = true;
            return dbOperationResult;
        }

        public async Task<DtoCreateEntityResponse> CreateFriendRequestAsync(int creatorId, DtoCreateFriendRequest createFriendRequest)
        {
            var createEntityResponse = new DtoCreateEntityResponse();
            var entity = new FriendRequest { 
                IssuerId = creatorId,
                ReceiverId = createFriendRequest.ReceiverId
            };
            _amiqContext.FriendRequests.Add(entity);
            await _amiqContext.SaveChangesAsync();
            var newFriendRequestQuery = _amiqContext.FriendRequests.Where(e => e.FriendRequestId == entity.FriendRequestId);
            var newFriendRequest = await APAutoMapper.Instance.ProjectTo<DtoFriendRequest>(newFriendRequestQuery).SingleAsync();
            createEntityResponse.Result = true;
            createEntityResponse.Entity = newFriendRequest;
            return createEntityResponse;
        }

        public bool IsFriendRequestCreatedByUser(int userId, Guid friendRequestId)
        {
            return _amiqContext.FriendRequests.Any(e => e.IssuerId == userId && e.FriendRequestId == friendRequestId);
        }
    }
}
