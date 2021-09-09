﻿using Amiq.Contracts;
using Amiq.DataAccess.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Business.Post
{
    public class BsPost
    {
        private DaPost _daPost = new DaPost();

        public async Task<DtoDeleteEntityResponse> DeleteAsync(Guid postId)
        {
            return await _daPost.DeleteAsync(postId);
        }
    }
}
