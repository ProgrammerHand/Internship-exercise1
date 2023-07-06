using System.Text;

namespace File_sending.Interfaces
{
    public interface ICSVHelperService
    {
        IEnumerable<T> ReadCSV<T>(Stream file);
        StringBuilder WriteCSV<T>(List<T> records);
    }
}
