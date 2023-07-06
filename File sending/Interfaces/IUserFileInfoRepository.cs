using File_sending.Models;

namespace File_sending.Interfaces
{
    public interface IUserFileInfoRepository
    {
        ICollection<UserFileInfo> GetAll ();

        UserFileInfo GetUserFileInfo(int id);

        UserFileInfo GetUserFileInfo(string name);

        bool UserFileInfoExist(string name);

        bool CreateUserFileInfo(UserFileInfo info);

        bool UpdateUserFileInfo(UserFileInfo info);

        bool Save();

    }
}
