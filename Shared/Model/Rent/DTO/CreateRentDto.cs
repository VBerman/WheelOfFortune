using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheelOfFortune.Shared.Model.Rent
{
    public class CreateRentDto
    {
        public int ClientId { get; set; }
        public int RealEstateId { get; set; }

        public DateTime StartRentDate { get; set; }
        public DateTime EndRentDate { get; set; }

    }
}
