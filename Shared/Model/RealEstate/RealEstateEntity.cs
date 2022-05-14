using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WheelOfFortune.Shared.Model.Record;
using WheelOfFortune.Shared.Model.User;

namespace WheelOfFortune.Shared.Model.RealEstate
{
    public class RealEstateEntity
    {
        public RealEstateEntity()
        {
            Records = new HashSet<RecordEntity>();
        }
        public int Id { get; set; }
        [MaxLength(150)]
        public string Name { get; set; }
        [MaxLength(350)]
        public string Address { get; set; }
        [MaxLength(150)]
        public string Security { get; set; }
        public bool HasParking { get; set; }

        [Range(0, 10000.00)]
        [Column(TypeName = "decimal(7,2)")]
        public decimal Area { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public string? AdditionalInfo { get; set; }

        public int LandlordId { get; set; }
        public virtual UserEntity Landlord { get; set; }

        public bool IsConfirmed { get; set; }

        public string? ImagePath { get; set; }

        public int? AdminConfirmedId { get; set; }

        public string? RejectionReason { get; set; }

        public virtual UserEntity? AdminConfirmed { get; set; }

        public virtual ICollection<RecordEntity> Records { get; set; }
    }
}
