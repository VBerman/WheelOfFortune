using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheelOfFortune.Shared.Model.Rent
{
    public class ReadRentDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string ClientFullName { get; set; }
        public int RealEstateId { get; set; }
        public string RealEstateAddress { get; set; }
        public DateTime StartRentDate { get; set; }
        public DateTime EndRentDate { get; set; }
        public bool IsDebt { get; set; }

        [DataType(DataType.Currency)]
        public decimal PriceInMonth { get; set; }
    }
}
