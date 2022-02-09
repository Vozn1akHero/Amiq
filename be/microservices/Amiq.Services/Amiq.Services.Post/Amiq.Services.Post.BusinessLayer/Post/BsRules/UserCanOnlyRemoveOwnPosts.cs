using Amiq.Services.Post.BusinessLayer.Utils;
using Amiq.Services.Post.DataAccessLayer.Models.Models;

namespace Amiq.Services.Post.BusinessLayer.Post.BsRules
{
    public class UserCanOnlyRemoveOwnPosts : IBsRule
    {
        public string ErrorContent => throw new NotImplementedException();

        private AmiqPostContext _amiqContext;
        private int _userId;
        private Guid _postId;

        public UserCanOnlyRemoveOwnPosts(AmiqPostContext amiqContext, int userId, Guid postId)
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
