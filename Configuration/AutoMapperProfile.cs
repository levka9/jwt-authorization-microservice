using AutoMapper;
using JWT_Auth.Microservice.Entities;
using JWT.Auth.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace JWT.Auth.Configuration
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateUserRequest, User>().ForMember(dest => dest.UserUserRole,
                                            y => y.Ignore());

            CreateMap<UpdateUserRequest, User>().ForMember(dest => dest.UserUserRole,
                                            y => y.Ignore());
        }
    }
}
