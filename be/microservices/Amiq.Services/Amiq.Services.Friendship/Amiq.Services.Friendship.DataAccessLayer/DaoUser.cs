using Amiq.Services.Friendship.Contracts.User;
using Amiq.Services.Friendship.DataAccessLayer.Models.Models;

namespace Amiq.Services.Friendship.DataAccessLayer
{
    public class DaoUser
    {
        private AmiqFriendshipContext _amiqFriendshipContext = new AmiqFriendshipContext();

        public void AddOrUpdate(DtoBasicUserInfo dtoBasicUserInfo)
        {
            var user = _amiqFriendshipContext.Users.Find(dtoBasicUserInfo.UserId);
            if(user == null)
            {
                _amiqFriendshipContext.Users.Add(new User
                {
                    UserId = dtoBasicUserInfo.UserId,
                    Name = dtoBasicUserInfo.Name,
                    Surname = dtoBasicUserInfo.Surname,
                    AvatarPath = dtoBasicUserInfo.AvatarPath
                });
            }
            else
            {
                user.Name = dtoBasicUserInfo.Name;
                user.Surname = dtoBasicUserInfo.Surname;
                user.AvatarPath = dtoBasicUserInfo.AvatarPath;
            }
            _amiqFriendshipContext.SaveChanges();
        }
    }
}
