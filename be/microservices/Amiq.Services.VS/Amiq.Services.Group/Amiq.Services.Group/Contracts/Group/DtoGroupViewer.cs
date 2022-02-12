namespace Amiq.Services.Group.Contracts.Group
{
    public class DtoGroupViewer
    {
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public EnGroupViewerRole GroupViewerRole { get; set; }
    }

    public enum EnGroupViewerRole
    {
        Creator, Admin, Participant, Guest, Blocked
    }
}
