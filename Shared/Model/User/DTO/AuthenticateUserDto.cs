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
        [Required(ErrorMessage = "Email обязателен")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Пароль обязателен")]
        [MinLength(3, ErrorMessage = "Минимальная длина пароля - 3 символа")]
        public string Password { get; set; }    
    }
}
