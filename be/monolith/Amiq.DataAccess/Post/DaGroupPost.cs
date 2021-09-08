using Amiq.Contracts.Post;
using Amiq.DataAccess.Models.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Amiq.DataAccess.Post
{
    public class DaGroupPost
    {
        private AmiqContext amiqContext = new AmiqContext();

        public async Task CreateAsync(DtoGroupPost dtoGroupPost)
        {
            amiqContext.GroupPosts.Add(new GroupPost
            {
                GroupId = dtoGroupPost.GroupId,
                Post = new DataAccess.Models.Models.Post
                {
                    TextContent = dtoGroupPost.Text
                }
            });
            await amiqContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid groupPostId)
        {
            var post = amiqContext.GroupPosts.SingleOrDefault(e => e.GroupPostId == groupPostId);
            if (post != null)
            {
                amiqContext.GroupPosts.Remove(post);
                await amiqContext.SaveChangesAsync();
            }
        }

        public async Task EditAsync(DtoEditGroupPostRequest dtoEditGroupPostRequest)
        {
            var post = amiqContext.GroupPosts.SingleOrDefault(e => e.GroupPostId == dtoEditGroupPostRequest.GroupPostId);
            if (post != null)
            {
                post.Post.TextContent = dtoEditGroupPostRequest.Text;
                //TODO
                await amiqContext.SaveChangesAsync();
            }
        }
    }
}
