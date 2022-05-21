using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheelOfFortune.Shared.ViewModel
{
    public class MessageModel
    {
        public string Message { get; set; }
        public int FromUserId { get; set; }
        public int ChatId { get; set; }

        public DateTime DateTime { get; set; }
    }
}
