using Amiq.Services.Common.Contracts;
using Amiq.Services.Post.Contracts.Post;
using Amiq.Services.Post.DataAccessLayer.Models;

namespace Amiq.Services.Post.DataAccessLayer.Post
{
    public class DaoPost
    {
        private AmiqPostContext _amiqContext = new AmiqPostContext();

        public async Task<DtoDeleteEntityResponse> DeleteByPostIdAsync(Guid postId)
        {
            var dtoDeleteEntityResponse = new DtoDeleteEntityResponse();
            var record = _amiqContext.Posts.Where(e => e.PostId == postId).FirstOrDefault();
            if (record != null)
            {
                _amiqContext.Posts.Remove(record);
                await _amiqContext.SaveChangesAsync();
                dtoDeleteEntityResponse.Entity = record;
                dtoDeleteEntityResponse.IsBusinessException = true;
            }
            return dtoDeleteEntityResponse;
        }

        public async Task EditAsync(DtoEditPostRequest dtoEditPostRequest)
        {
            var record = _amiqContext.Posts.Where(e => e.PostId == dtoEditPostRequest.PostId).FirstOrDefault();
            if (record != null)
            {
                record.TextContent = dtoEditPostRequest.Text;
                record.EditedBy = dtoEditPostRequest.EditedBy;
                await _amiqContext.SaveChangesAsync();
            }
        }
    }
}
