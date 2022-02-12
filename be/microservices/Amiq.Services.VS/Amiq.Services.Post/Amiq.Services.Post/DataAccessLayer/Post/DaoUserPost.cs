using Amiq.Services.Post.Common;
using Amiq.Services.Post.Contracts.Post;
using Amiq.Services.Post.Contracts.Utils;
using Amiq.Services.Post.DataAccessLayer.Models;
using Amiq.Services.Post.DataAccessLayer.Models.Models;
using Amiq.Services.Post.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Amiq.Services.Post.DataAccessLayer.Post
{
    public class DaoUserPost
    {
        private AmiqPostContext _amiqContext = new AmiqPostContext();

        public async Task<DtoListResponseOf<DtoUserPost>> GetUserPostsAsync(int userId, DtoPaginatedRequest dtoPaginatedRequest)
        {
            DtoListResponseOf<DtoUserPost> result = new();
            result.Length = await _amiqContext.UserPosts.Where(e => e.UserId == userId).CountAsync();
            var query = _amiqContext.UserPosts.Where(e => e.UserId == userId)
                .Paginate(dtoPaginatedRequest.Page, dtoPaginatedRequest.Count)
                .OrderByDescending(e => e.Post.CreatedAt)
                .Include(e => e.Post)
                .Include(e => e.Post.Comments);
            result.Entities = await AmiqPostAutoMapper.Instance.ProjectTo<DtoUserPost>(query).ToListAsync();
            return result;
        }

        public async Task EditAsync(DtoEditUserPostRequest dtoEditUserPostRequest)
        {
            var post = _amiqContext.UserPosts.Include(e => e.Post).SingleOrDefault(e => e.PostId == dtoEditUserPostRequest.PostId);
            if (post != null)
            {
                post.Post.TextContent = dtoEditUserPostRequest.TextContent;
                post.Post.EditedAt = DateTime.Now;
                post.Post.EditedBy = dtoEditUserPostRequest.UserId;
                await _amiqContext.SaveChangesAsync();
            }
        }

        public async Task<DtoUserPost> CreateAsync(int issuerId, DtoPostCreation dtoPost)
        {
            var userPost = AmiqPostAutoMapper.Instance.Map<UserPost>(dtoPost);
            var post = AmiqPostAutoMapper.Instance.Map<Models.Post>(dtoPost);
            userPost.UserId = issuerId;
            _amiqContext.Posts.Add(post);
            await _amiqContext.SaveChangesAsync();

            userPost.PostId = post.PostId;
            _amiqContext.UserPosts.Add(userPost);
            await _amiqContext.SaveChangesAsync();

            //IQueryable query = _amiqContext.UserPosts.Where(e => e.PostId == userPost.PostId);
            //var res = APAutoMapper.Instance.ProjectTo<DtoUserPost>(query).SingleOrDefault();

            /*var res = _amiqContext.UserPosts.Where(e => e.PostId == userPost.PostId)
                 .Select(i => new DtoUserPost
             {
                 PostId = i.PostId,
                 Author = new DtoBasicUserInfo
                 {
                     UserId = i.User.UserId,
                     Name = i.User.Name,
                     Surname = i.User.Surname,
                     AvatarPath = i.User.AvatarPath
                 },
                 AvatarPath = i.User.AvatarPath,
                 TextContent = i.Post.TextContent,
                 CreatedAt = i.Post.CreatedAt,
                 EditedAt = i.Post.EditedAt,
                 EditedBy = i.Post.EditedBy,
                 Comments = new List<DtoPostComment>()
             }).First();*/

            return AmiqPostAutoMapper.Instance
                .ProjectTo<DtoUserPost>(_amiqContext.UserPosts.Where(e => e.PostId == userPost.PostId))
                .Single();
        }
    }
}
