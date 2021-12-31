using Amiq.Contracts.Post;
using Amiq.DataAccessLayer.Models;
using Amiq.DataAccessLayer.Models.Models;
using Amiq.Services.Post.Contracts.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Services.Post.DataAccessLayer.Post
{
    public class DaoPost
    {
        private AmiqContext _amiqContext = new AmiqContext();

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
