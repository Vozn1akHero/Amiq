namespace Amiq.Services.Group.Contracts.Group
{
    public class DtoEditGroupData
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string AvatarSrc { get; set; }
        public string Description { get; set; }
        public List<DtoDescriptionBlock> DescriptionBlocks { get; set; }
    }
}
