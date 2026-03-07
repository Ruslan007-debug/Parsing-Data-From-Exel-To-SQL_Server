using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripETL.Models;
using Microsoft.Data.SqlClient;

namespace TripETL.Services
{
    public class SqlBulkInserter
    {
        private readonly string connectionString;

        public SqlBulkInserter(string _connectionString)
        {
            connectionString = _connectionString;
        }

        public void InsertBatch(List<TripRecord> records)
        {
            var table = new DataTable();

            table.Columns.Add("PickupDatetime", typeof(DateTime));
            table.Columns.Add("DropoffDatetime", typeof(DateTime));
            table.Columns.Add("PassengerCount", typeof(short));
            table.Columns.Add("TripDistance", typeof(double));
            table.Columns.Add("StoreAndForwardFlag", typeof(string));
            table.Columns.Add("PULocationID", typeof(int));
            table.Columns.Add("DOLocationID", typeof(int));
            table.Columns.Add("FareAmount", typeof(decimal));
            table.Columns.Add("TipAmount", typeof(decimal));


            foreach (var record in records)
            {
                table.Rows.Add(
                    record.PickupDatetime,
                    record.DropoffDatetime,
                    record.PassengerCount ?? (object)DBNull.Value,
                    record.TripDistance,
                    record.StoreAndForwardFlag,
                    record.PULocationID,
                    record.DOLocationID,
                    record.FareAmount,
                    record.TipAmount
                );
            }

            using var connection = new SqlConnection(connectionString);
            connection.Open();

            using var bulk = new SqlBulkCopy(connection)
            {
                DestinationTableName = "Trips",
                BatchSize = 10000

            };

            bulk.ColumnMappings.Add("PickupDatetime", "PickupDatetime");
            bulk.ColumnMappings.Add("DropoffDatetime", "DropoffDatetime");
            bulk.ColumnMappings.Add("PassengerCount", "PassengerCount");
            bulk.ColumnMappings.Add("TripDistance", "TripDistance");
            bulk.ColumnMappings.Add("StoreAndForwardFlag", "StoreAndForwardFlag");
            bulk.ColumnMappings.Add("PULocationID", "PULocationID");
            bulk.ColumnMappings.Add("DOLocationID", "DOLocationID");
            bulk.ColumnMappings.Add("FareAmount", "FareAmount");
            bulk.ColumnMappings.Add("TipAmount", "TipAmount");

            bulk.WriteToServer(table);

        }   

    }
}
