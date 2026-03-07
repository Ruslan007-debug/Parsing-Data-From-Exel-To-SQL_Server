using System;
using System.Collections.Generic;
using CsvHelper;
using System.Globalization;

namespace TripETL.Services
{
    public class CsvStreamReader
    {
        public IEnumerable<dynamic> ReadFile(string path)
        {
            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            foreach (var record in csv.GetRecords<dynamic>())
            {
                yield return record;
            }
        }
    }
}
