﻿using AmicaPlus.DataAccess.Models.Models;
using AmicaPlus.ResultSets.Auth;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmicaPlus.Mapping
{
    public class UserProfile : APProfile
    {
        public UserProfile(){
            CreateTwoWayMap<RsUserInfo, User>();
        }
    }
}