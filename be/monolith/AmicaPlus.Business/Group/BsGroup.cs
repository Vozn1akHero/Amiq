using AmicaPlus.Business.Utils;
using AmicaPlus.Contracts.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmicaPlus.Business
{
    public class BsGroup : BsServiceBase
    {
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
    }
}
