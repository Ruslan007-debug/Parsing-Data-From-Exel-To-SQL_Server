using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripETL.Models
{
    public class TripRecord
    {
        public DateTime PickupDatetime { get; set; }

        public DateTime DropoffDatetime { get; set; }

        public short? PassengerCount { get; set; }

        public double TripDistance { get; set; }

        public string? StoreAndForwardFlag { get; set; }

        public int PULocationID { get; set; }

        public int DOLocationID { get; set; }

        public decimal FareAmount { get; set; }

        public decimal TipAmount { get; set; }
    }
}
