namespace Amiq.Services.Group.Contracts
{
    public class DtoGroupParticipant
    {
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string AvatarPath { get; set; }
    }
}
