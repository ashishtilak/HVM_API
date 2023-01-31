using HVM_API.Dto;
using HVM_API.Models;
using AutoMapper;

namespace HVM_API.Models
{
    public class AppModelMappingProfile : Profile
    {
        public AppModelMappingProfile()
        {
            CreateMap<Units, UnitsDto>();
            CreateMap<UnitsDto, Units>();

            CreateMap<Users, UserDto>();
            CreateMap<UserDto, Users>();

            CreateMap<Roles, RolesDto>();
            CreateMap<RolesDto, Roles>();

            CreateMap<AuthObjects, AuthObjectsDto>();
            CreateMap<AuthObjectsDto, AuthObjects>();

            CreateMap<RoleAuths, RoleAuthsDto>();
            CreateMap<RoleAuthsDto, RoleAuths>();

            CreateMap<RoleUsers, RoleUsersDto>();
            CreateMap<RoleUsersDto, RoleUsers>();
        }
    }
}
