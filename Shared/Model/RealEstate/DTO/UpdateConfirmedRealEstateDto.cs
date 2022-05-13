
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheelOfFortune.Shared.Model.RealEstate
{
    public class UpdateConfirmedRealEstateDto
    {
        public int Id { get; set; }

        public bool IsConfirmed { get; set; }
        public string? RejectionReason { get; set; }

    }
}
