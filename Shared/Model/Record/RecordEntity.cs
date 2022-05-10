using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WheelOfFortune.Shared.Model.RealEstate;
using WheelOfFortune.Shared.Model.User;

namespace WheelOfFortune.Shared.Model.Record
{
    public class RecordEntity
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public virtual UserEntity Client { get; set; }
        
        public int RealStateId { get; set; }

        public virtual RealEstateEntity RealEstate { get; set; }

        public DateTime DateTime { get; set; }


    }
}
