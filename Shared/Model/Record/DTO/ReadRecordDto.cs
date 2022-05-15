
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheelOfFortune.Shared.Model.Record
{
    public class ReadRecordDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int LandlordId { get; set; }
        public int RealEstateId { get; set; }

        public string Address { get; set; }

        public string ClientFullName { get; set; }
        public string LandlordFullName { get; set; }

        public DateTime DateTime { get; set; }

    }
}
