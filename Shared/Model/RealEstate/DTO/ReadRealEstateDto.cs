using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheelOfFortune.Shared.Model.RealEstate
{
    public class ReadRealEstateDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Наименование обязательно для заполнения")]
        [MaxLength(150, ErrorMessage = "Максимальная длина - 150 символов")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Адрес обязателен для заполнения")]
        [MaxLength(350, ErrorMessage = "Максимальная длина - 350 символов")]
        public string Address { get; set; }
        [MaxLength(150, ErrorMessage = "Максимальная длина - 150 символов")]
        [Required(ErrorMessage = "Безопасность обязательна для заполнения")]
        public string Security { get; set; }
        [Required(ErrorMessage = "Необходимо указать наличие парковки")]
        public bool HasParking { get; set; }
        [Required(ErrorMessage = "Площадь обязательна для заполнения")]
        [Range(5, 10000.00, ErrorMessage = "Минимальное значение - 5, максимальное - 10000")]
        public decimal Area { get; set; }
        [Required(ErrorMessage = "Цена обязательна для заполнения")]
        [Range(5, 100000000.00, ErrorMessage = "Минимальное значение - 5, максимальное - 100000000")]
        public decimal Price { get; set; }
        public string? AdditionalInfo { get; set; }

        public int? LandlordId { get; set; }
        public string? LandLordFullName { get; set; }
        [Required]
        public bool IsConfirmed { get; set; }
        public string? RejectionReason { get; set; }

        public string? ImagePath { get; set; }

    }
}
