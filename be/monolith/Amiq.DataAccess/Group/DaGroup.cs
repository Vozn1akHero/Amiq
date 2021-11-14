using Amiq.Common.DbOperation;
using Amiq.Contracts;
using Amiq.Contracts.Core;
using Amiq.Contracts.Group;
using Amiq.Contracts.User;
using Amiq.DataAccess.Models.Models;
using Amiq.Mapping;
using AutoMapper.QueryableExtensions;
using Microsoft.Data.SqlClient.DataClassification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Amiq.DataAccess.Group
{
    public class DaGroup
    {
        private AmiqContext _amiqContext;
        private DaGroupParticipant _daParticipant;

        public DaGroup()
        {
            _amiqContext = new AmiqContext();
        }

        public async Task<List<DtoGroupParticipant>> GetGroupParticipantsAsync(int groupId)
        {
            IQueryable query = _amiqContext.GroupParticipants
                .Where(e => e.GroupId == groupId)
                .Join(_amiqContext.Users,
                    participant => participant.UserId,
                    user => user.UserId,
                    (participant, user) => new { Participant = participant, User = user });
            var data = await APAutoMapper.Instance.ProjectTo<DtoGroupParticipant>(query).ToListAsync();
            return data;
        }

        public async Task<DtoGroupCard> CreateGroupAsync(int creatorId, DtoCreateGroup dtoCreateGroup)
        {
            var entity = new Models.Models.Group
            {
                Name = dtoCreateGroup.Name,
                Description = dtoCreateGroup.Description,
                CreatedBy = creatorId
            };
            _amiqContext.Add(entity);
            await _amiqContext.SaveChangesAsync();
            await _daParticipant.JoinGroupAsync(creatorId, entity.GroupId);
            DtoGroupCard dtoGroupCard = _amiqContext.Groups.Where(e=>e.GroupId == entity.GroupId)
                .Select(g => new DtoGroupCard
                {
                    GroupId = g.GroupId,
                    Name = g.Name,
                    AvatarSrc = g.AvatarSrc,
                    Description = g.Description,
                    ParticipantsCount = g.GroupParticipants.Count,
                    IsHidden = false,
                    IsRequestCreatorParticipant = true
                }).Single();
            return dtoGroupCard;
        }

        public async Task<IEnumerable<DtoGroupCard>> GetByNameAsync(int userId, string name)
        {
            return await _amiqContext.Groups.AsNoTracking().Where(e => e.Name.StartsWith(name))
                .Select(g => new DtoGroupCard
                {
                    GroupId = g.GroupId,
                    Name = g.Name,
                    AvatarSrc = g.AvatarSrc,
                    Description = g.Description,
                    ParticipantsCount = g.GroupParticipants.Count,
                    IsHidden = g.HiddenGroups.Any(hg => hg.UserId == userId && hg.GroupId == g.GroupId),
                    IsRequestCreatorParticipant = g.GroupParticipants.Any(gp => gp.UserId == userId && gp.GroupId == g.GroupId)
                }).ToListAsync();
        }

        public async Task<DtoGroup> GetGroupById(int groupId)
        {
            var query = _amiqContext.Groups.Where(e => e.GroupId == groupId);
            var data = await APAutoMapper.Instance.ProjectTo<DtoGroup>(query).SingleOrDefaultAsync();
            return data;
        }

        public async Task<DtoEditEntityResponse> EditAsync(DtoEditGroupDataRequest dtoEditGroupDataRequest)
        {
            DtoEditEntityResponse result = new();
            var group = _amiqContext.Groups.SingleOrDefault(e => e.GroupId == dtoEditGroupDataRequest.GroupId);
            try
            {
                if (group != null)
                {
                    group.Name = dtoEditGroupDataRequest.Name;
                    group.AvatarSrc = dtoEditGroupDataRequest.AvatarSrc;
                    group.Description = dtoEditGroupDataRequest.Description;
                    await _amiqContext.SaveChangesAsync();
                    result.Entity = APAutoMapper.Instance.Map<DtoGroup>(group);
                    result.Result = true;
                }
            } catch(Exception ex)
            {
                result.Result = false;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
