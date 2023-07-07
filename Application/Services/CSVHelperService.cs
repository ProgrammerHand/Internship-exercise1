using CsvHelper;
using CsvHelper.Configuration;
using File_sending.Interfaces;
using System.Globalization;
using System.Text;

namespace File_sending.Services
{
    public class CSVHelperService : ICSVHelperService
    {
        public IEnumerable<T> ReadCSV<T>(Stream file)
        {
            var reader = new StreamReader(file);
            var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture){ Delimiter = ";", Encoding = Encoding.UTF8 });
            csv.Read();
            csv.ReadHeader();
            var records = csv.GetRecords<T>();
            return records;
        }
        
        public StringBuilder WriteCSV<T>(List<T> records)
        {
            var sb = new StringBuilder();
            StringWriter csvString = new StringWriter(sb);
            using (var csv = new CsvWriter(csvString, CultureInfo.InvariantCulture))
            {
                csv.WriteField("sep=,", false);
                csv.NextRecord();
                csv.WriteRecords(records);
            }
            return sb;
        }
    }
}
