using Amiq.Services.User.Contracts.User;
using Amiq.Services.User.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Amiq.Services.User.DataAccessLayer
{
    public class DaoBlockedUser
    {
        private AmiqUserContext _amiqContext = new AmiqUserContext();

        public async Task<IEnumerable<DtoBlockedUser>> GetBlockedUsersByUserIdAsync(int userId)
        {
            return await _amiqContext.BlockedUsers.Where(e => e.IssuerId == userId)
                .Select(e => new DtoBlockedUser
                {
                    UserId = e.DestUserId,
                    Name = e.DestUser.Name,
                    Surname = e.DestUser.Surname
                })
                .ToListAsync();
        }

        public async Task UnblockUser(DtoUserBlockRequest dtoUserBlockRequest)
        {
            var record = _amiqContext.BlockedUsers.SingleOrDefault(e => e.IssuerId == dtoUserBlockRequest.IssuerId
                && e.DestUserId == dtoUserBlockRequest.DestUserId);
            if (record != null)
            {
                _amiqContext.Remove(record);
                await _amiqContext.SaveChangesAsync();
            }
        }

        public void BlockUser(DtoUserBlockRequest dtoUserBlockRequest)
        {
            _amiqContext.Add(new BlockedUser
            {
                DestUserId = dtoUserBlockRequest.DestUserId,
                IssuerId = dtoUserBlockRequest.IssuerId
            });
            _amiqContext.SaveChanges();
        }

        public DtoBlockedUser GetBlockedUserByIssuerId(DtoUserBlockRequest dtoUserBlockRequest)
        {
            return _amiqContext.BlockedUsers.Where(e => e.IssuerId == dtoUserBlockRequest.IssuerId
                && e.DestUserId == dtoUserBlockRequest.DestUserId)
                .Select(e => new DtoBlockedUser
                {
                    UserId = e.DestUserId,
                    Name = e.DestUser.Name,
                    Surname = e.DestUser.Surname
                })
                .SingleOrDefault();
        }

        public bool OneWayBlockExists(int issuerId, int userId)
        {
            return _amiqContext.BlockedUsers.Any(e => e.IssuerId == issuerId && e.DestUserId == userId
            || e.IssuerId == userId && e.DestUserId == issuerId);
        }

        public bool IsUserBlockedByAnotherUser(int issuerId, int userId)
        {
            return _amiqContext.BlockedUsers.Any(e => e.IssuerId == userId && e.DestUserId == issuerId);
        }
    }
}
