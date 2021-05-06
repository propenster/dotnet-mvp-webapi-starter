using AutoMapper;
using DotNetWebAPIMVPStarter.Models;
using DotNetWebAPIMVPStarter.Models.Blog;
using DotNetWebAPIMVPStarter.Models.Blog.RequestDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetWebAPIMVPStarter.Profiles
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<RegisterModel, User>();
            CreateMap<User, UserModel>();
            CreateMap<UpdateUserModel, User>();

            //post mapping
            CreateMap<CreatePostRequest, Post>();

        }
    }
}
