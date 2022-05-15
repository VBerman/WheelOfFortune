using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheelOfFortune.Shared.Model.Record
{
    public class CreateRecordDto
    {
        public int ClientId { get; set; }
        public int RealEstateId { get; set; }
        public DateTime DateTime { get; set; }

    }
}
