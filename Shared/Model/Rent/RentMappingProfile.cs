using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
namespace WheelOfFortune.Shared.Model.Rent
{
    public class RealEstateMappingProfile : Profile
    {
        public RealEstateMappingProfile()
        {
            CreateMap<CreateRentDto, RentEntity>();
           
        }
    }
}
