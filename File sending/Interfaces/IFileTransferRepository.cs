using File_sending.Models;

namespace File_sending.Interfaces
{
    public interface IFileTransferRepository
    {
        Task<ICollection<Models.FileSpecs>> GetAll ();

        Task<Models.FileSpecs> GetUserFileInfo(int id);

        Task<Models.FileSpecs> GetUserFileInfo(string name);

        Task<bool> UserFileInfoExist(string name);

        Task<bool> CreateUserFileInfo(Models.FileSpecs info);

        Task<bool> UpdateUserFileInfo(Models.FileSpecs info);

        Task<bool> Save();

    }
}
