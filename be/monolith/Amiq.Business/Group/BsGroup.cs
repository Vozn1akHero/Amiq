﻿using Amiq.Business.Utils;
using Amiq.Contracts.Group;
using Amiq.DataAccess.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Business
{
    public class BsGroup : BsServiceBase
    {
        private DaGroup _daGroup = new DaGroup();

        public DtoGroup GetGroupDataById(int groupId)
        {
            var group = new DtoGroup();
            return group;
        }

        public async Task<IEnumerable<DtoGroup>> GetParticipatedGroupByUserId(int userId)
        {
            var output = new List<DtoGroup>();

            return output;
        }

        public async Task<IEnumerable<DtoGroup>> GetByName(string name)
        {
            return await _daGroup.GetByNameAsync(name);
        }

        public async Task<DtoGroup> GetGroupById(int groupId)
        {
            return await _daGroup.GetGroupById(groupId);
        }
    }
}