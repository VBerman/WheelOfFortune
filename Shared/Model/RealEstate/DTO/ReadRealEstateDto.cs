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
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }
        [Required]
        [MaxLength(350)]
        public string Address { get; set; }
        [MaxLength(150)]
        [Required]
        public string Security { get; set; }
        [Required]
        public bool HasParking { get; set; }
        [Required]
        [Range(5, 10000.00)]
        public decimal Area { get; set; }
        [Required]
        [Range(5, 100000000.00)]
        public decimal Price { get; set; }
        public string? AdditionalInfo { get; set; }

        public int? LandlordId { get; set; }
        public string? LandLordFullName { get; set; }
        [Required]
        public bool IsConfirmed { get; set; }
        public string? RejectionReason { get; set; }

    }
}
