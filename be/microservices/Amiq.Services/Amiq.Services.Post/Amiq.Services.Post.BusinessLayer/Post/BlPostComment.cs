using Amiq.Services.Post.BusinessLayer.Utils;
using Amiq.Services.Post.Contracts.Post;
using Amiq.Services.Post.Contracts.Utils;
using Amiq.Services.Post.DataAccessLayer.Post;

namespace Amiq.Services.Post.BusinessLayer.Post
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
