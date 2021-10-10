﻿using Amiq.Contracts.User;
using System;
using System.Collections.Generic;

namespace Amiq.Contracts.Post
{
    public class DtoPost
    {
        public Guid PostId { get; set; }
        public string TextContent { get; set; }
        //public int UserId { get; set; }
        public DtoUserInfo Author { get; set; }
        public int? EditedBy { get; set; }
        public DateTime? EditedAt {  get; set; }
        public DateTime CreatedAt { get; set; }
        public string AvatarPath { get; set; }
        public bool HasMoreCommentsThanRecent { get; set; }
        public List<DtoPostComment> RecentComments { get; set; }
    }
}