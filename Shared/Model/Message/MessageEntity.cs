using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WheelOfFortune.Shared.Model.Chat;
using WheelOfFortune.Shared.Model.User;

namespace WheelOfFortune.Shared.Model.Message
{
    public class MessageEntity
    {
        public int Id { get; set; }

        public int FromUserId { get; set; }
        
        public virtual UserEntity FromUser { get; set; }


        public int ChatId { get; set; }

        public virtual ChatEntity Chat { get; set; }

        public string Message { get; set; }


    }
}
