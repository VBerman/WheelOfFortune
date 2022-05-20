using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WheelOfFortune.Shared.Model.Message;
using WheelOfFortune.Shared.Model.RealEstate;
using WheelOfFortune.Shared.Model.User;

namespace WheelOfFortune.Shared.Model.Chat
{
    public class ChatEntity
    {
        public int Id { get; set; }

        public int RealEstateId { get; set; }
        public virtual RealEstateEntity RealEstate { get; set; }
        public ICollection<UserEntity> Users { get; set; }
        public ICollection<MessageEntity> Messages { get; set; }
    }
}
