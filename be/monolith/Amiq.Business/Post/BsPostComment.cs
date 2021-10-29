using Amiq.Business.Utils;
using Amiq.Common.Enums;
using Amiq.Contracts.Post;
using Amiq.Contracts.Utils;
using Amiq.DataAccess.Post;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Business.Post
{
    public class BsPostComment : BusinessLayerBase
    {
        private DaPostComment daPostComment = new DaPostComment();

        public async Task<IEnumerable<DtoPostComment>> GetUserPostCommentsAsync(Guid postId, DtoPaginatedRequest dto)
        {
             return await daPostComment.GetUserPostCommentAsync(postId, dto);
        }

        public async Task<IEnumerable<DtoGroupPostComment>> GetGroupPostCommentsAsync(Guid postId, DtoPaginatedRequest dto)
        {
            return await daPostComment.GetGroupPostCommentsAsync(postId, dto);
        }

        public async Task<DtoPostComment> CreateAsync(int authorId, DtoCreatePostComment newPostComment)
        {
            return await daPostComment.CreateAsync(authorId, newPostComment);
        }

        public async Task<DtoGroupPostComment> CreateGroupPostCommentAsync(int authorId, DtoCreateGroupPostComment dtoCreateGroupPostComment)
        {
            return await daPostComment.CreateGroupPostCommentAsync(authorId, dtoCreateGroupPostComment);
        }

        public async Task<DtoPostComment> DeleteAsync(Guid postCommentId)
        {
            return await daPostComment.DeleteAsync(postCommentId);
        }

        public async Task<DtoPostComment> EditAsync(Guid postCommentId, string text)
        {
             return await daPostComment.EditAsync(postCommentId, text);
        }

    }
}
