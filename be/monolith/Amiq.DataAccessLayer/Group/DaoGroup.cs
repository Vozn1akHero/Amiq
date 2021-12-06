using Amiq.Common.DbOperation;
using Amiq.Contracts;
using Amiq.Contracts.Core;
using Amiq.Contracts.Group;
using Amiq.Contracts.User;
using Amiq.DataAccessLayer.Models.Models;
using Amiq.Mapping;
using AutoMapper.QueryableExtensions;
using Microsoft.Data.SqlClient.DataClassification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Amiq.DataAccessLayer.Group
{
    public class DaoGroup
    {
        private AmiqContext _amiqContext = new AmiqContext();
        private DaoGroupParticipant _daParticipant = new DaoGroupParticipant();

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
                CreatedBy = creatorId,
                AvatarSrc = "group.jpg"
            };
            _amiqContext.Add(entity);
            await _amiqContext.SaveChangesAsync();
            await _daParticipant.JoinGroupAsync(creatorId, entity.GroupId);
            var dtoGroupCard = _amiqContext.Groups.Where(e=>e.GroupId == entity.GroupId)
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
            /*return _amiqContext.Groups.Where(e => e.GroupId == groupId)
                .Select(e => new DtoGroup { 
                
                });*/
        }

        public DtoEditEntityResponse Edit(DtoEditGroupData dtoEditGroupDataRequest)
        {
            DtoEditEntityResponse result = new();
            var group = _amiqContext.Groups
                .Include(g => g.GroupDescriptionBlocks)
                .ThenInclude(g=>g.TextBlock)
                .SingleOrDefault(e => e.GroupId == dtoEditGroupDataRequest.GroupId);
            try
            {
                if (group != null)
                {
                    group.Name = dtoEditGroupDataRequest.Name;
                    group.Description = dtoEditGroupDataRequest.Description;

                    foreach (var groupDescriptionBlock in group.GroupDescriptionBlocks)
                    {
                        var passedBlock = dtoEditGroupDataRequest
                            .DescriptionBlocks
                            .Where(e=> e.TextBlockId != Guid.Empty)
                            .SingleOrDefault(e => e.TextBlockId == groupDescriptionBlock.TextBlockId);
                        if (passedBlock!=null)
                        {
                            groupDescriptionBlock.TextBlock.Header = passedBlock.Header;
                            groupDescriptionBlock.TextBlock.Content = passedBlock.Content;
                        }
                    }

                    foreach(var descriptionBlock in dtoEditGroupDataRequest.DescriptionBlocks.Where(e => e.TextBlockId == Guid.Empty))
                        group.GroupDescriptionBlocks.Add(new GroupDescriptionBlock { 
                            GroupId = group.GroupId,
                            TextBlock = new TextBlock { 
                                Header = descriptionBlock.Header,
                                Content = descriptionBlock.Content
                            }
                        });
                    _amiqContext.Entry(group).State = EntityState.Modified;
                    _amiqContext.SaveChanges();
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

        public DtoEditEntityResponse ChangeGroupAvatar(int groupId, string avatarPath)
        {
            DtoEditEntityResponse result = new();
            var group = _amiqContext.Groups.Find(groupId);
            if (group != null)
            {
                group.AvatarSrc = avatarPath;
                _amiqContext.SaveChanges();
                result.Entity = APAutoMapper.Instance.Map<DtoGroup>(group);
                result.Result = true;
            }
            return result;
        }
    }
}
