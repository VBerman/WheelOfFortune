using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WheelOfFortune.Shared.Model.User;

namespace WheelOfFortune.Shared.Model.Tokens
{
    public class RefreshTokenEntity 
    {
        public int Id { get; set; }
        [ForeignKey("UserEntity")]
        public int UserId { get; set; }

        public virtual UserEntity User { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}
