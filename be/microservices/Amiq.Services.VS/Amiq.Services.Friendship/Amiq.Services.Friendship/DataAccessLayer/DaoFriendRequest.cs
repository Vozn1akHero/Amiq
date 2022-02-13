using Amiq.Services.Common.Contracts;
using Amiq.Services.Common.DbOperation;
using Amiq.Services.Common.Enums;
using Amiq.Services.Friendship.Contracts.Friendship;
using Amiq.Services.Friendship.Contracts.User;
using Amiq.Services.Friendship.DataAccessLayer.Models;
using Amiq.Services.Friendship.Mapping;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Amiq.Services.Friendship.DataAccessLayer
{
    public class DaoFriendRequest
    {
        private AmiqFriendshipContext _amiqContext = new AmiqFriendshipContext();

        public async Task<FriendRequest> GetFriendRequestByIdAsync(Guid friendRequestId)
        {
            return await _amiqContext.FriendRequests.SingleAsync(e=>e.FriendRequestId == friendRequestId);
        }

        public async Task<IEnumerable<DtoFriendRequest>> GetFriendRequestsAsync(int userId, FriendRequestType friendRequestType)
        {
            Expression<Func<FriendRequest, bool>> whereBody = e => e.IssuerId == userId;

            if (friendRequestType == FriendRequestType.Creator)
                whereBody = e => e.IssuerId == userId;
            else if (friendRequestType == FriendRequestType.Receiver)
                whereBody = e => e.ReceiverId == userId;

            return await _amiqContext.FriendRequests.Where(whereBody).Select(e => new DtoFriendRequest
            {
                FriendRequestId = e.FriendRequestId,
                Creator = new DtoBasicUserInfo
                { 
                    UserId = e.Issuer.UserId,
                    Name = e.Issuer.Name,
                    Surname = e.Issuer.Surname,
                    AvatarPath = e.Issuer.AvatarPath
                },
                Receiver = new DtoBasicUserInfo
                {
                    UserId = e.Receiver.UserId,
                    Name = e.Receiver.Name,
                    Surname = e.Receiver.Surname,
                    AvatarPath = e.Receiver.AvatarPath
                },
                IssuerId = e.IssuerId,
                ReceiverId = e.ReceiverId
            }).ToListAsync();
            /*return await AmiqFriendshipAutoMapper
                .Instance
                .ProjectTo<DtoFriendRequest>(_amiqContext.FriendRequests.Where(whereBody))
                .ToListAsync();*/
        }

        public async Task<DtoCreateEntityResponse> AcceptFriendRequestAsync(Guid friendRequestId)
        {
            var createEntityResponse = new DtoCreateEntityResponse();
            using var t = await _amiqContext.Database.BeginTransactionAsync();
            var frEntity = _amiqContext.FriendRequests.Single(e => e.FriendRequestId == friendRequestId);
            try
            {
                _amiqContext.FriendRequests.Remove(frEntity);
                var friendship = new Models.Friendship
                {
                    FirstUserId = frEntity.ReceiverId,
                    SecondUserId = frEntity.IssuerId
                };
                _amiqContext.Friendships.Add(friendship);
                
                await _amiqContext.SaveChangesAsync();

                await t.CommitAsync();

                createEntityResponse.Entity = friendship;
            }
            catch (Exception ex)
            {
                createEntityResponse.Message = ex.Message;
                await t.RollbackAsync();
            }
            createEntityResponse.Result = true;
            return createEntityResponse;
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
            var entity = new FriendRequest
            {
                IssuerId = creatorId,
                ReceiverId = createFriendRequest.ReceiverId
            };
            _amiqContext.FriendRequests.Add(entity);
            await _amiqContext.SaveChangesAsync();
            /*var newFriendRequestQuery = _amiqContext.FriendRequests.Where(e => e.FriendRequestId == entity.FriendRequestId);
            var newFriendRequest = await AmiqFriendshipAutoMapper.Instance.ProjectTo<DtoFriendRequest>(newFriendRequestQuery).SingleAsync();*/
            createEntityResponse.Result = true;
            createEntityResponse.Entity = _amiqContext.FriendRequests.Where(e => e.FriendRequestId == entity.FriendRequestId)
                .Select(e=> new DtoFriendRequest { 
                    Creator = new DtoBasicUserInfo { 
                       // UserId = e.
                    }
                })
                .Single();
            return createEntityResponse;
        }

        public bool IsFriendRequestCreatedByUser(int userId, Guid friendRequestId)
        {
            return _amiqContext.FriendRequests.Any(e => e.IssuerId == userId && e.FriendRequestId == friendRequestId);
        }

        public DtoFriendRequest GetFriendRequestByUserIds(int fUserId, int sUserId)
        {
            /*IQueryable queryable = _amiqContext.FriendRequests.Where(e => e.IssuerId == fUserId && e.ReceiverId == sUserId
                || e.IssuerId == sUserId && e.ReceiverId == fUserId);
            return AmiqFriendshipAutoMapper.Instance.ProjectTo<DtoFriendRequest>(queryable).Single();*/
            return _amiqContext.FriendRequests.AsNoTracking().Where(e => e.IssuerId == fUserId && e.ReceiverId == sUserId
                || e.IssuerId == sUserId && e.ReceiverId == fUserId)
                .Select(e => new DtoFriendRequest { 
                    FriendRequestId = e.FriendRequestId,
                    IssuerId = e.IssuerId,
                    ReceiverId = e.ReceiverId
                })
                .Single();
        }

        public bool FriendRequestExists(int fUserId, int sUserId)
        {
            return _amiqContext.FriendRequests.Any(e => e.IssuerId == fUserId && e.ReceiverId == sUserId
                || e.IssuerId == sUserId && e.ReceiverId == fUserId);
        }
    }
}
