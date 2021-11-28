using Amiq.Contracts;
using Amiq.Contracts.Group;
using Amiq.Contracts.Group.Enums;
using Amiq.Contracts.User;
using Amiq.Contracts.Utils;
using Amiq.DataAccessLayer.Group;
using Amiq.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Business
{
    public class BlGroupParticipant
    {
        private DaoGroupParticipant _daGroupParticipant;

        public BlGroupParticipant()
        {
            _daGroupParticipant = new DaoGroupParticipant();
        }

        public async Task<DtoListResponseOf<DtoGroupCard>> GetUserGroupsByUserIdAsync(int userId,
            DtoPaginatedRequest dtoPaginatedRequest, EnGroupFilterType filterType)
        {
            if (filterType == EnGroupFilterType.All)
                return await _daGroupParticipant.GetUserGroupsByUserIdAsync(userId, dtoPaginatedRequest);
            else if (filterType == EnGroupFilterType.Administered)
                return await _daGroupParticipant.GetAdministeredUserGroupsByUserIdAsync(userId, dtoPaginatedRequest);
            else if (filterType == EnGroupFilterType.Nonadministered)
                return await _daGroupParticipant.GetNonAdministeredUserGroupsByUserIdAsync(userId, dtoPaginatedRequest);
            else if (filterType == EnGroupFilterType.Hidden)
                return await _daGroupParticipant.GetHiddenUserGroupsByUserIdAsync(userId, dtoPaginatedRequest);
            else return await _daGroupParticipant.GetUserGroupsByUserIdAsync(userId, dtoPaginatedRequest);
        }

        public async Task<DtoDeleteEntityResponse> DeleteParticipantAsync(int userId, int groupId)
        {
            var deleteEntityResponse = new DtoDeleteEntityResponse();
            var groupParticipant = _daGroupParticipant.GetGroupParticipant(userId, groupId);
            await _daGroupParticipant.DeleteParticipantAsync(groupParticipant);
            deleteEntityResponse.Entity = APAutoMapper.Instance.Map<DtoGroupParticipant>(groupParticipant);
            return deleteEntityResponse;
        }

        public async Task LeaveGroupAsync(int userId, int groupId)
        {
            await _daGroupParticipant.LeaveGroupAsync(userId, groupId);
        }

        public async Task JoinGroupAsync(int userId, int groupId)
        {
            await _daGroupParticipant.JoinGroupAsync(userId, groupId);
        }

        public async Task<DtoGroupParticipant> GetGroupParticipantAsync(DtoMinifiedGroupParticipant dtoSimplifiedGroupParticipant)
        {
            return await _daGroupParticipant.GetGroupParticipantAsync(dtoSimplifiedGroupParticipant);
        }

        public async Task<DtoGroupViewer> GetGroupViewerByUserIdAsync(int userId, int groupId)
        {
            var result = await _daGroupParticipant.GetGroupViewerByUserIdAsync(userId, groupId);
            return result;
        }

        public async Task<DtoListResponseOf<DtoGroupParticipant>> GetGroupParticipantsAsync(int groupId, DtoPaginatedRequest paginatedRequest)
        {
            return await _daGroupParticipant.GetGroupParticipantsAsync(groupId, paginatedRequest);
        }
    }
}
