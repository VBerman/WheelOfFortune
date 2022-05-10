using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheelOfFortune.Shared.Model.User
{
    public class AuthenticateUserDto
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }    
    }
}
