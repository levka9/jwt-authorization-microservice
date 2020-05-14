using AutoMapper;
using JWT.Auth.Entities;
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
                //.ForMember(dest => dest.ApplicationId,
                //                            y => y.MapFrom(x => (int)x.Application))
                //                                .ForMember(dest => dest.Application,
                //                            y => y.Ignore());
        }
    }
}
