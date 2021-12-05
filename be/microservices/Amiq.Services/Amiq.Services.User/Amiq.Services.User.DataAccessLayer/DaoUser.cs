using Amiq.Contracts;
using Amiq.Contracts.User;
using Amiq.Contracts.Utils;
using Amiq.DataAccessLayer.Models.Models;
using Amiq.Mapping;
using Amiq.Services.User.Contracts.User;
using Amiq.Services.User.DataAccessLayer.Models.Models;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.DataAccessLayer.User
{
    public class DaoUser
    {
        private AmiqUserContext _amiqContext = new AmiqUserContext();

        public async Task<IEnumerable<DtoUserDescriptionBlock>> GetUserDescriptionAsync(int userId)
        {
            //var output = new DtoUserDescription();
            var output = await _amiqContext.UserDescriptionBlocks
                .Where(e => e.UserId == userId)
                .Select(e => new DtoUserDescriptionBlock {
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
                .Where(e => e.UserId == userId)
                .Select(e => new DtoExtendedUserInfo {
                    UserId = e.UserId,
                    Name = e.Name,
                    Surname = e.Surname,
                    Email = e.Email,
                    AvatarPath = e.AvatarPath,
                    Birthdate = e.Birthdate,
                    ShortDescription = e.ShortDescription,
                    UserDescriptionBlocks = e.UserDescriptionBlocks.Select(d=> new DtoUserDescriptionBlock { 
                        TextBlockId = d.TextBlockId,
                        Header = d.TextBlock.Header,
                        Content = d.TextBlock.Content
                    }),
                    BlockedByIssuer = e.BlockedUserDestUsers.Any(i => i.DestUserId == userId && i.IssuerId == requestCreatorId),
                    IssuerBlocked = e.BlockedUserIssuers.Any(i => i.IssuerId == userId && i.DestUserId == requestCreatorId),
                    IssuerReceivedFriendRequest = e.FriendRequestIssuers.Any(i => i.ReceiverId == requestCreatorId && i.IssuerId == userId),
                    IssuerSentFriendRequest = e.FriendRequestReceivers.Any(i => i.IssuerId == requestCreatorId && i.ReceiverId == userId),
                    IsIssuerFriend = e.FriendshipFirstUsers.Any(d => (d.FirstUserId == userId && d.SecondUserId == requestCreatorId) || (d.FirstUserId == requestCreatorId && d.SecondUserId == userId))
                    || e.FriendshipSecondUsers.Any(d => (d.FirstUserId == userId && d.SecondUserId == requestCreatorId) || (d.FirstUserId == requestCreatorId && d.SecondUserId == userId)),
                }).SingleOrDefaultAsync();

            return result;
        }

        

        public async Task<IEnumerable<DtoUserSearchResult>> SearchAsync(int issuerId, string text, DtoPaginatedRequest paginatedRequest)
        {
            /*var result = (from u in _amiqContext.Users.AsNoTracking()
                     join bu in _amiqContext.BlockedUsers.AsNoTracking()
                     on u.UserId equals bu.IssuerId into joineduBu
                     from bu in joineduBu.DefaultIfEmpty()

                     join bu1 in _amiqContext.BlockedUsers.AsNoTracking()
                     on u.UserId equals bu1.DestUserId into joineduBu1
                     from bu1 in joineduBu1.DefaultIfEmpty()

                     join fr in _amiqContext.FriendRequests.AsNoTracking()
                     on u.UserId equals fr.ReceiverId into joinedUFr
                     from fr in joinedUFr.DefaultIfEmpty()

                     join fri in _amiqContext.FriendRequests.AsNoTracking()
                     on u.UserId equals fri.IssuerId into joinedUFri
                     from fri in joinedUFri.DefaultIfEmpty()

                     join frf in _amiqContext.Friendships.AsNoTracking()
                     on u.UserId equals frf.FirstUserId into joinedUFrf
                     from frf in joinedUFrf.DefaultIfEmpty()

                     join frs in _amiqContext.Friendships.AsNoTracking()
                     on u.UserId equals frs.SecondUserId into joinedUFrs
                     from frs in joinedUFrs.DefaultIfEmpty()

                     where ((u.Name + " " + u.Surname).ToUpper().StartsWith(text.ToUpper()) 
                        && (bu.IssuerId == issuerId || bu1.DestUserId == issuerId || fr.ReceiverId == issuerId 
                            || fri.IssuerId == issuerId)
                     )

                     select new DtoUserSearchResult
                     {
                         FriendRequestCanBeIssued = bu1.DestUserId == issuerId || bu.IssuerId == issuerId,
                         IssuerSentFriendRequest = fri != null,
                     })
                     .Skip((paginatedRequest.Page - 1) * paginatedRequest.Count)
                     .Take(paginatedRequest.Count);*/

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
                        IssuerBlocked = e.BlockedUserIssuers.Any(i => i.DestUserId == issuerId),
                        IssuerReceivedFriendRequest = e.FriendRequestIssuers.Any(i => i.ReceiverId == issuerId),
                        IssuerSentFriendRequest = e.FriendRequestReceivers.Any(i => i.IssuerId == issuerId),
                        IsIssuerFriend = e.FriendshipFirstUsers.Any(e => e.SecondUserId == issuerId) || e.FriendshipSecondUsers.Any(e => e.FirstUserId == issuerId),
                    })
                    .Skip((paginatedRequest.Page - 1) * paginatedRequest.Count)
                    .Take(paginatedRequest.Count);
                //var result = await APAutoMapper.Instance.ProjectTo<DtoUserInfo>(query).ToListAsync();
                return result;
            });
        }
    }
}
