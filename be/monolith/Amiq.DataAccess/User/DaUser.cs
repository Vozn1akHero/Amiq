using Amiq.Contracts.User;
using Amiq.DataAccess.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.DataAccess.User
{
    public class DaUser
    {
        private AmiqContext _amiqContext = new AmiqContext();

        public async Task<DtoUserDescription> GetUserDescriptionAsync(int userId)
        {
            var output = new DtoUserDescription();
            var output2 = await _amiqContext.UserDescriptionBlocks
                .Where(e => e.UserId == userId)
                .Select(e => new DtoUserDescriptionBlock {
                    //Title = e.
                })
                .ToListAsync();
            output.UserDescriptionBlocks = output2;

            return output;
        }
    }
}
