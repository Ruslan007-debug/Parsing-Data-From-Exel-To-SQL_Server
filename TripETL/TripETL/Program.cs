using TripETL.Models;

namespace TripETL
{
    public class Program
    {
        static void Main(string[] args)
        {
            string csvPath = "sample-cab-data.csv";

            string connectionString = "Data Source=DESKTOP-DJK38G4\\SQLEXPRESS;Initial Catalog=TripsDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";

            
            var reader = new Services.CsvStreamReader();
            var editor = new Services.EditingService();
            var duplicatesFinder = new Services.FindDuplicates();
            var inserter = new Services.SqlBulkInserter(connectionString);
            var batch = new List<TripRecord>();
            var duplicateList = new List<TripRecord>();
            const int batchSize = 10000;

            foreach (var row in reader.ReadFile(csvPath))
            {
                if (!editor.TryEdit(row, out TripRecord record))
                {
                    continue;
                }

                if (duplicatesFinder.IsDuplicate(record))
                {
                    duplicateList.Add(record);
                    continue;
                }

                batch.Add(record);
                if (batch.Count >= batchSize)
                {
                    inserter.InsertBatch(batch);
                    batch.Clear();
                }
            }

            if (batch.Count > 0)
            {
                inserter.InsertBatch(batch);
            }

            string duplicatePath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                @"..\..\..\duplicates.csv"
            );

            File.WriteAllLines(
                duplicatePath,
                duplicateList.Select(d =>
                    $"{d.PickupDatetime},{d.DropoffDatetime},{d.PassengerCount}")
            );

            Console.WriteLine("ETL task done");



        }
    }
}
