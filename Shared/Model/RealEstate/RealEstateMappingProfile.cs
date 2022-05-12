using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
namespace WheelOfFortune.Shared.Model.RealEstate
{
    public class RealEstateMappingProfile : Profile
    {
        public RealEstateMappingProfile()
        {
            CreateMap<CreateRealEstateDto, RealEstateEntity>().ReverseMap();
            CreateMap<UpdateRealEstateDto, RealEstateEntity>().ReverseMap();
            CreateMap<UpdateConfirmedRealEstateDto, RealEstateEntity>().ReverseMap();
            CreateMap<RealEstateEntity, ReadRealEstateDto>();

        }
    }
}
