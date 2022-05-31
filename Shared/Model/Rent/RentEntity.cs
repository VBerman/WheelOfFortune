using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WheelOfFortune.Shared.Model.RealEstate;
using WheelOfFortune.Shared.Model.User;

namespace WheelOfFortune.Shared.Model.Rent
{
    public class RentEntity
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int RealEstateId { get; set; }

        public virtual UserEntity Client { get; set; }  
        public virtual RealEstateEntity RealEstate { get; set; }

        public DateTime StartRentDate { get; set; }
        public DateTime EndRentDate { get; set; }

        public bool IsDebt { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PriceInMonth { get; set; }
    }
}
