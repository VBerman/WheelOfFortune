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
        [MaxLength(255, ErrorMessage = "Максимальная длина - 255")]
        [EmailAddress]
        [Required(ErrorMessage = "Email обязателен для заполнения")]
        public string Email { get; set; }
        [MaxLength(100, ErrorMessage = "Максимальная длина - 100")]
        [DataType(DataType.Password)]   
        [Required(ErrorMessage = "Пароль обязателен для заполнения")]
        public string Password { get; set; }
        [MaxLength(255, ErrorMessage = "Максимальная длина - 255")]
        [Required(ErrorMessage = "ФИО обязательно для заполнения")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Роль обязательна для заполнения")]
        public Role Role { get; set; }
    }
}
