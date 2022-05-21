using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WheelOfFortune.Shared.ViewModel;

namespace WheelOfFortune.Shared.Model.Message
{
    public class MessageMappingProfile : Profile
    {
        public MessageMappingProfile()
        {
            CreateMap<MessageEntity, MessageModel>().ReverseMap();
        }
    }
}
