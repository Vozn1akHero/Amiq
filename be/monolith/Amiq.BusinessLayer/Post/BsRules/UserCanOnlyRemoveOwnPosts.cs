using Amiq.BusinessLayer.Utils;
using Amiq.DataAccessLayer.Models.Models;
using Amiq.DataAccessLayer.Post;
using System;
using System.Linq;

namespace Amiq.BusinessLayer.Post.BsRules
{
    public class UserCanOnlyRemoveOwnPosts : IBsRule
    {
        public string ErrorContent => throw new NotImplementedException();

        private AmiqContext _amiqContext;
        private int _userId;
        private Guid _postId;

        public UserCanOnlyRemoveOwnPosts(AmiqContext amiqContext, int userId, Guid postId)
        {
            _amiqContext = amiqContext;
            _userId = userId;
            _postId = postId;
        }

        public bool CheckBsRule()
        {
            return _amiqContext.UserPosts.Any(e => e.UserId == _userId && e.UserPostId == _postId);
        }
    }
}
