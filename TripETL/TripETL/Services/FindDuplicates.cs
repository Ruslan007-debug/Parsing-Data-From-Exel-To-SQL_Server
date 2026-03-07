using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripETL.Models;

namespace TripETL.Services
{
    public class FindDuplicates
    {
        private readonly HashSet<string> passed = new();

        public bool IsDuplicate(TripRecord record)
        {
            string key = $"{record.PickupDatetime}-{record.DropoffDatetime}-{record.PassengerCount}";

            if (passed.Contains(key))
            {
                return true;
            }
            passed.Add(key);
            return false;
        }
    }
}
