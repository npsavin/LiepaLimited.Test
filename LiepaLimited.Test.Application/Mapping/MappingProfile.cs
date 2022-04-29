using AutoMapper;
using LiepaLimited.Test.Application.Dto;
using LiepaLimited.Test.Domain;

namespace LiepaLimited.Test.Application.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<UserInfo, UserInfoDto>()
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()));

            CreateMap<UserInfoDto, UserInfo> ()
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.MapToStatus()));
        }

     
    }
}
