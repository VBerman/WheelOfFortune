
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
namespace WheelOfFortune.Shared.Model.Record
{
    public class RecordMappingProfile : Profile
    {
        public RecordMappingProfile()
        {
            CreateMap<CreateRecordDto, RecordEntity>();
            CreateMap<RecordEntity, ReadRecordDto>();
            
        }
    }
}
