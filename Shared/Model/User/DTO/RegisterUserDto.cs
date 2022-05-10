using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WheelOfFortune.Shared.Enums;

namespace WheelOfFortune.Shared.Model.User
{
    public class RegisterUserDto
    {
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; }
        [MaxLength(100)]
        [DataType(DataType.Password)]   
        public string Password { get; set; }
        [MaxLength(255)]
        public string FullName { get; set; }
        public Role Role { get; set; }
    }
}
