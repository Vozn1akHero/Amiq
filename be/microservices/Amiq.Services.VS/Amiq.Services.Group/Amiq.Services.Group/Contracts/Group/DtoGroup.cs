namespace Amiq.Services.Group.Contracts.Group
{
    public class DtoGroup
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string AvatarSrc { get; set; }
        public string Description { get; set; }
        public int ParticipantsCount { get; set; }
        public bool IsHidden { get; set; }
        public List<DtoDescriptionBlock> DescriptionBlocks { get; set; }
    }
}
