using File_sending.Models;

namespace File_sending.Interfaces
{
    public interface IUserFileInfoRepository
    {
        Task<ICollection<UserFileInfo>> GetAll ();

        Task<UserFileInfo> GetUserFileInfo(int id);

        Task<UserFileInfo> GetUserFileInfo(string name);

        Task<bool> UserFileInfoExist(string name);

        Task<bool> CreateUserFileInfo(UserFileInfo info);

        Task<bool> UpdateUserFileInfo(UserFileInfo info);

        Task<bool> Save();

    }
}
