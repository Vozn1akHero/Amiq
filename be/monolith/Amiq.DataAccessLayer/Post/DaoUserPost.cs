using Amiq.Common;
using Amiq.Contracts;
using Amiq.Contracts.Post;
using Amiq.Contracts.User;
using Amiq.Contracts.Utils;
using Amiq.DataAccessLayer.Models.Models;
using Amiq.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.DataAccessLayer.Post
{
    public class DaoUserPost
    {
        private AmiqContext _amiqContext = new AmiqContext();

        public async Task<DtoListResponseOf<DtoUserPost>> GetUserPostsAsync(int userId, DtoPaginatedRequest dtoPaginatedRequest)
        {
            DtoListResponseOf<DtoUserPost> result = new();
            result.Length = await _amiqContext.UserPosts.Where(e => e.UserId == userId).CountAsync();
            var query = _amiqContext.UserPosts.Where(e=>e.UserId == userId)
                .Paginate(dtoPaginatedRequest.Page, dtoPaginatedRequest.Count)
                .OrderByDescending(e=>e.Post.CreatedAt)
                .Include(e=>e.Post)
                .Include(e=>e.Post.Comments);
            result.Entities = await APAutoMapper.Instance.ProjectTo<DtoUserPost>(query).ToListAsync();
            return result;
        }

        public async Task EditAsync(DtoEditUserPostRequest dtoEditUserPostRequest)
        {
            var post = _amiqContext.UserPosts.Include(e=>e.Post).SingleOrDefault(e=>e.PostId == dtoEditUserPostRequest.PostId);
            if(post != null)
            {
                post.Post.TextContent = dtoEditUserPostRequest.TextContent;
                post.Post.EditedAt = DateTime.Now;
                post.Post.EditedBy = dtoEditUserPostRequest.UserId;
                await _amiqContext.SaveChangesAsync();
            }
        }

        public async Task<DtoUserPost> CreateAsync(int issuerId, DtoPostCreation dtoPost)
        {
            var userPost = APAutoMapper.Instance.Map<UserPost>(dtoPost);
            var post = APAutoMapper.Instance.Map<Models.Models.Post>(dtoPost);
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
           
            return APAutoMapper.Instance
                .ProjectTo<DtoUserPost>(_amiqContext.UserPosts.Where(e => e.PostId == userPost.PostId))
                .Single();
        }
    }
}
