using Amiq.BusinessLayer.Utils;
using Amiq.DataAccessLayer.Group;
using Amiq.DataAccessLayer.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.BusinessLayer.Group.Rules
{
    internal class MustBeAdminRule : IBsRuleAsync
    {
        private DaoGroupParticipant _daoGroupParticipant;
        private int _userId;
        private int _groupId;

        public MustBeAdminRule(DaoGroupParticipant daoGroupParticipant, int userId, int groupId)
        {
            _daoGroupParticipant = daoGroupParticipant;
            _userId = userId;
            _groupId = groupId;
        }

        public string ErrorContent => "Użytkownik nie jest administratorem";

        public async Task<bool> CheckBsRuleAsync()
        {
            return await _daoGroupParticipant.IsAdminAsync(_userId, _groupId);
        }
    }
}
