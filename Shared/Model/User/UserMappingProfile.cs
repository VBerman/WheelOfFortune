using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
namespace WheelOfFortune.Shared.Model.User
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserEntity, ReadUserDto>();
            CreateMap<RegisterUserDto, UserEntity>().ReverseMap();
        }
    }
}
