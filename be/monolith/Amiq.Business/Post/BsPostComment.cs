using Amiq.Business.Utils;
using Amiq.Contracts.Post;
using Amiq.Contracts.Utils;
using Amiq.DataAccess.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Business.Post
{
    public class BsPostComment : BsServiceBase
    {
        private DaPostComment daPostComment = new DaPostComment();

        public async Task<IEnumerable<DtoPostComment>> GetPostCommentsAsync(Guid postId, DtoPaginatedRequest dto)
        {
            return await daPostComment.GetPostCommentAsync(postId, dto);
        }
    }
}
