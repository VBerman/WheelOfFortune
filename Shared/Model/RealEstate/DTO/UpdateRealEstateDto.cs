using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheelOfFortune.Shared.Model.RealEstate
{
    public class UpdateRealEstateDto
    {
        public int Id { get; set; }
        [MaxLength(150)]
        public string Name { get; set; }
        [MaxLength(350)]
        public string Address { get; set; }
        [MaxLength(150)]
        public string Security { get; set; }
        public bool HasParking { get; set; }
        [Range(0, 10000.00)]
        public decimal Area { get; set; }
        [DataType(DataType.Currency)]
        [Range(0, 100000000)]
        public decimal Price { get; set; }
        public string? AdditionalInfo { get; set; }

    }
}
