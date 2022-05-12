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
        public string Name { get; set; }
        public string Address { get; set; }
        public string Security { get; set; }
        public bool HasParking { get; set; }
        public decimal Area { get; set; }
        public decimal Price { get; set; }
        public string? AdditionalInfo { get; set; }
        public int LandlordId { get; set; }
        public string LandLordFullName { get; set; }
        public bool IsConfirmed { get; set; }
        public string? RejectionReason { get; set; }

    }
}
