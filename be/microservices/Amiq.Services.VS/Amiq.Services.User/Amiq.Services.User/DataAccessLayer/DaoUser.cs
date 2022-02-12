using Amiq.Services.Common.Contracts;
using Amiq.Services.User.Contracts.User;
using Amiq.Services.User.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Amiq.Services.User.DataAccessLayer
{
    public class DaoUser
    {
        private AmiqUserContext _amiqContext = new AmiqUserContext();

        public async Task<IEnumerable<DtoUserDescriptionBlock>> GetUserDescriptionAsync(int userId)
        {
            //var output = new DtoUserDescription();
            var output = await _amiqContext.UserDescriptionBlocks
                .Where(e => e.UserId == userId)
                .Select(e => new DtoUserDescriptionBlock
                {
                    TextBlockId = e.TextBlockId,
                    Header = e.TextBlock.Header,
                    Content = e.TextBlock.Content
                })
                .ToListAsync();
            //output.UserDescriptionBlocks = output2;

            return output;
        }

        public async Task<DtoExtendedUserInfo> GetUserByIdAsync(int requestCreatorId, int userId)
        {
            var result = await _amiqContext.Users
                .AsNoTracking()
                .Where(e => e.UserId == userId)
                .Select(e => new DtoExtendedUserInfo
                {
                    UserId = e.UserId,
                    Name = e.Name,
                    Surname = e.Surname,
                    Email = e.Email,
                    AvatarPath = e.AvatarPath,
                    Birthdate = e.Birthdate,
                    ShortDescription = e.ShortDescription,
                    UserDescriptionBlocks = e.UserDescriptionBlocks.Select(d => new DtoUserDescriptionBlock
                    {
                        TextBlockId = d.TextBlockId,
                        Header = d.TextBlock.Header,
                        Content = d.TextBlock.Content
                    }),
                    BlockedByIssuer = e.BlockedUserDestUsers.Any(i => i.DestUserId == userId && i.IssuerId == requestCreatorId),
                    IssuerBlocked = e.BlockedUserIssuers.Any(i => i.IssuerId == userId && i.DestUserId == requestCreatorId),
                    /*IssuerReceivedFriendRequest = e.FriendRequestIssuers.Any(i => i.ReceiverId == requestCreatorId && i.IssuerId == userId),
                    IssuerSentFriendRequest = e.FriendRequestReceivers.Any(i => i.IssuerId == requestCreatorId && i.ReceiverId == userId),
                    IsIssuerFriend = e.FriendshipFirstUsers.Any(d => (d.FirstUserId == userId && d.SecondUserId == requestCreatorId) || (d.FirstUserId == requestCreatorId && d.SecondUserId == userId))
                    || e.FriendshipSecondUsers.Any(d => (d.FirstUserId == userId && d.SecondUserId == requestCreatorId) || (d.FirstUserId == requestCreatorId && d.SecondUserId == userId)),*/
                }).SingleOrDefaultAsync();

            return result;
        }

        public async Task<DtoBasicUserInfo> GetBasicUserDataByIdAsync(int userId)
        {
            return await _amiqContext.Users
               .AsNoTracking()
               .Where(e => e.UserId == userId)
               .Select(e => new DtoBasicUserInfo
               {
                   UserId = e.UserId,
                   Name = e.Name,
                   Surname = e.Surname,
                   AvatarPath = e.AvatarPath,
               })
               .SingleOrDefaultAsync();
         }

        public async Task<IEnumerable<DtoUserSearchResult>> SearchAsync(int issuerId, string text, DtoPaginatedRequest paginatedRequest)
        {
            return await Task.Run(() =>
            {
                var result = _amiqContext
                    .Users
                    .Where(u => (u.Name + " " + u.Surname).ToUpper().StartsWith(text.ToUpper()))
                    .Select(e => new DtoUserSearchResult
                    {
                        UserId = e.UserId,
                        Name = e.Name,
                        Surname = e.Surname,
                        AvatarPath = e.AvatarPath,
                        BlockedByIssuer = e.BlockedUserDestUsers.Any(i => i.IssuerId == issuerId),
                        IssuerBlocked = e.BlockedUserIssuers.Any(i => i.DestUserId == issuerId)
                    })
                    .Skip((paginatedRequest.Page - 1) * paginatedRequest.Count)
                    .Take(paginatedRequest.Count);
                return result;
            });
        }
    }
}
