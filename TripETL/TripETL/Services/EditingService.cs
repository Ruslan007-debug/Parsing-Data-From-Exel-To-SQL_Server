using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripETL.Models;

namespace TripETL.Services
{
    public class EditingService
    {
        private TimeZoneInfo _timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

        public bool TryEdit(dynamic row, out TripRecord record)
        {
            record = null;
            if (!DateTime.TryParse(row.tpep_pickup_datetime?.ToString(), out DateTime pickup))
            {
                return false;
            }
            if (!DateTime.TryParse(row.tpep_dropoff_datetime?.ToString(), out DateTime dropoff))
            {
                return false;
            }
            if (!short.TryParse(row.passenger_count?.ToString(), out short passangers))
            {
                return false;
            }
            if (!double.TryParse(row.trip_distance?.ToString(), out double distance))
            {
                return false;
            }

            if (!decimal.TryParse(row.fare_amount?.ToString(), out decimal fare))
            {
                return false;
            }

            if (!decimal.TryParse(row.tip_amount?.ToString(), out decimal tip))
            { 
                return false; 
            }

            if (!int.TryParse(row.PULocationID?.ToString(), out int startOfTrip))
            { 
                return false; 
            }

            if (!int.TryParse(row.DOLocationID?.ToString(), out int endOfTrip))
            { 
                return false;
            }

            string? flag = row.store_and_fwd_flag?.ToString().Trim();

            flag = flag switch
            {
                "Y" => "Yes",
                "N" => "No",
                _ => flag
            };

            pickup = TimeZoneInfo.ConvertTime(pickup, _timeZoneInfo);
            dropoff = TimeZoneInfo.ConvertTime(dropoff, _timeZoneInfo);

            record = new TripRecord
            {
                PickupDatetime = pickup,
                DropoffDatetime = dropoff,
                PassengerCount = passangers,
                TripDistance = distance,
                StoreAndForwardFlag = flag,
                PULocationID = startOfTrip,
                DOLocationID = endOfTrip,
                FareAmount = fare,
                TipAmount = tip
            };

            return true;


        }
    }
}
