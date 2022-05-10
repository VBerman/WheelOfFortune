using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WheelOfFortune.Shared.Enums;
using WheelOfFortune.Shared.Model.RealEstate;
using WheelOfFortune.Shared.Model.Record;

namespace WheelOfFortune.Shared.Model.User
{
    public class UserEntity
    {
        public UserEntity()
        {
            RealEstates = new HashSet<RealEstateEntity>();
            RealEstatesConfirmed = new HashSet<RealEstateEntity>();
            Records = new HashSet<RecordEntity>();
        }
        public int Id { get; set; }
        [MaxLength(255)]
        public string Email { get; set; }
        [MaxLength(100)]
        public string Password { get; set; }
        [MaxLength(255)]
        public string FullName { get; set; }
        public Role Role { get; set; }

        public virtual ICollection<RealEstateEntity> RealEstates { get; set; }
        
        public virtual ICollection<RecordEntity> Records { get; set; }

        public virtual ICollection<RealEstateEntity> RealEstatesConfirmed { get; set; }
    }
}
