using Amiq.Contracts.Post;
using Amiq.Contracts.Utils;
using Amiq.DataAccess.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.DataAccess.Post
{
    public class DaPostComment
    {
        private AmiqContext _amiqContext = new AmiqContext();

        public async Task<IEnumerable<DtoPostComment>> GetPostCommentAsync(Guid postId, DtoPaginatedRequest dtoPaginatedRequest)
        {
            //todo: procedura
            throw new NotImplementedException();
        }
    }
}
