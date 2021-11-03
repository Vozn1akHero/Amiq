﻿using Amiq.Contracts;
using Amiq.Contracts.Group;
using Amiq.Contracts.Group.Enums;
using Amiq.Contracts.Utils;
using Amiq.DataAccess.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Business
{
    public class BsGroupParticipant
    {
        private DaGroupParticipant _daGroupParticipant;

        public BsGroupParticipant()
        {
            _daGroupParticipant = new DaGroupParticipant();
        }

        public async Task<List<DtoGroup>> GetUserGroupsByUserIdAsync(int userId,
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

        public async Task LeaveGroupAsync(int userId, int groupId)
        {
            await _daGroupParticipant.LeaveGroupAsync(userId, groupId);
        }

        public async Task JoinGroupAsync(DtoJoinGroup dtoJoinGroup)
        {
            await _daGroupParticipant.JoinGroupAsync(dtoJoinGroup);
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

        public async Task BlockUserAsync(int userId, int groupId)
        {
             await _daGroupParticipant.BlockUserAsync(userId, groupId);
        }
    }
}
