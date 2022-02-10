namespace Amiq.Services.User.Amqp.IntegrationEvents
{
    public class UserModificationEvent : IntegrationEvent
    {
        public int UserId { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string AvatarPath { get; private set; }

        public UserModificationEvent(int userId, string name, string surname, string avatarPath)
        {
            UserId = userId;
            Name = name;
            Surname = surname;
            AvatarPath = avatarPath;
        }
    }
}
