using Amiq.Services.Group.Contracts;
using Amiq.Services.Group.Contracts.Utils;
using Amiq.Services.Group.DataAccessLayer.Models.Models;
using Amiq.Services.Group.Mapping;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Amiq.Services.Group.DataAccessLayer
{
    public class DaoGroup
    {
        private AmiqGroupContext _amiqContext = new AmiqGroupContext();
        private DaoGroupParticipant _daParticipant = new DaoGroupParticipant();

        public async Task<List<DtoGroupParticipant>> GetGroupParticipantsAsync(int groupId)
        {
            IQueryable query = _amiqContext.GroupParticipants
                .Where(e => e.GroupId == groupId)
                .Join(_amiqContext.Users,
                    participant => participant.UserId,
                    user => user.UserId,
                    (participant, user) => new { Participant = participant, User = user });
            var data = await AmiqGroupAutoMapper.Instance.ProjectTo<DtoGroupParticipant>(query).ToListAsync();
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
            var data = await AmiqGroupAutoMapper.Instance.ProjectTo<DtoGroup>(query).SingleOrDefaultAsync();
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
                    result.Entity = AmiqGroupAutoMapper.Instance.Map<DtoGroup>(group);
                    result.Result = true;
                }
            } catch(Exception ex)
            {
                result.Result = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public DtoEditEntityResponse Edit(DtoEditGroupData dtoEditGroupDataRequest)
        {
            DtoEditEntityResponse result = new();
            var group = _amiqContext.Groups
                .Include(g => g.GroupDescriptionBlocks)
                .ThenInclude(g => g.TextBlock)
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
                            .Where(e => e.TextBlockId != Guid.Empty)
                            .SingleOrDefault(e => e.TextBlockId == groupDescriptionBlock.TextBlockId);
                        if (passedBlock != null)
                        {
                            groupDescriptionBlock.TextBlock.Header = passedBlock.Header;
                            groupDescriptionBlock.TextBlock.Content = passedBlock.Content;
                        }
                    }

                    foreach (var descriptionBlock in dtoEditGroupDataRequest.DescriptionBlocks.Where(e => e.TextBlockId == Guid.Empty))
                        group.GroupDescriptionBlocks.Add(new GroupDescriptionBlock
                        {
                            GroupId = group.GroupId,
                            TextBlock = new TextBlock
                            {
                                Header = descriptionBlock.Header,
                                Content = descriptionBlock.Content
                            }
                        });
                    _amiqContext.Entry(group).State = EntityState.Modified;
                    _amiqContext.SaveChanges();
                    result.Entity = AmiqGroupAutoMapper.Instance.Map<DtoGroup>(group);
                    result.Result = true;
                }
            }
            catch (Exception ex)
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
            if(group != null)
            {
                group.AvatarSrc = avatarPath;
                _amiqContext.SaveChanges();
                result.Entity = AmiqGroupAutoMapper.Instance.Map<DtoGroup>(group);
                result.Result = true;
            }
            return result;
        }
    }
}
