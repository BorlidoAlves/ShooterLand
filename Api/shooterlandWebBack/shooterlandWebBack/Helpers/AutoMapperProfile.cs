using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using shooterlandWebBack.Entity;
using shooterlandWebBack.Models;
using shooterlandWebBack.Models.User;
using shooterlandWebBack.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shooterlandWebBack.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterModel, User>();
            CreateMap<UserUpdateModel, User>();
            CreateMap<AuthenticateModel, User>();
            CreateMap<User, UserModel>();

            CreateMap<AchievCreateModel, Achievement>();
            CreateMap<AchievUpdateModel, Achievement>();

        }
    }
}
