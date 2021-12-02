﻿namespace Amiq.Services.Group.Contracts
{
    public class DtoGroupCard
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string AvatarSrc { get; set; }
        public string Description { get; set; }
        public int ParticipantsCount { get; set; }
        public bool IsHidden { get; set; }
        public bool IsRequestCreatorParticipant { get; set; }
    }
}
