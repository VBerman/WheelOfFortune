using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WheelOfFortune.Shared.Enums;
using WheelOfFortune.Shared.Model.RealEstate;

namespace WheelOfFortune.Shared.Model.User
{
    public class ProfileUserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public Role Role { get; set; }
        public IEnumerable<ReadRealEstateDto> RealEstates { get; set; }
        
        public IEnumerable<ReadRealEstateDto> NotConfirmedRealEstates { get; set; }


    }
}
