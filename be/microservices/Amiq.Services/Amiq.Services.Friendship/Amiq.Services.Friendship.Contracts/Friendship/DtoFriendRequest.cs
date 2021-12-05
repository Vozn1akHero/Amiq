﻿using Amiq.Contracts.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Services.Friendship.Contracts.Friendship
{
    public class DtoFriendRequest
    {
        public Guid FriendRequestId { get; set; }
        public DtoBasicUserInfo Creator { get; set; }
        public DtoBasicUserInfo Receiver { get; set; }

        /*public int IssuerId { get; set; }
        public int ReceiverId { get; set; }*/
    }
}
