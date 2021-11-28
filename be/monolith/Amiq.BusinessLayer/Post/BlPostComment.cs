using Amiq.Business.Utils;
using Amiq.Common.Enums;
using Amiq.Contracts;
using Amiq.Contracts.Post;
using Amiq.Contracts.Utils;
using Amiq.DataAccessLayer.Post;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Business.Post
{
    public class BlPostComment : BusinessLayerBase
    {
        private DaoPostComment daPostComment = new DaoPostComment();

        public async Task<DtoListResponseOf<DtoPostComment>> GetUserPostCommentsAsync(Guid postId, DtoPaginatedRequest dto)
        {
             return await daPostComment.GetUserPostCommentAsync(postId, dto);
        }

        public async Task<DtoListResponseOf<DtoGroupPostComment>> GetGroupPostCommentsAsync(Guid postId, DtoPaginatedRequest dto)
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
