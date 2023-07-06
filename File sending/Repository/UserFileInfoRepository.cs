using File_sending.Data;
using File_sending.Interfaces;
using File_sending.Models;

namespace File_sending.Repository
{
    public class UserFileInfoRepository : IUserFileInfoRepository
    {
        private readonly AppDbContext _context;

        public UserFileInfoRepository(AppDbContext context)
        {
            _context = context;
        }

        public ICollection<UserFileInfo> GetAll() 
        {
            return _context.UserFileInfo.OrderBy(p => p.Id).ToList();
        }

        public UserFileInfo GetUserFileInfo(int id)
        {
            return _context.UserFileInfo.Where(p => p.Id == id).FirstOrDefault();
        }

        public UserFileInfo GetUserFileInfo(string name)
        {
            return _context.UserFileInfo.Where(p => p.Name == name).FirstOrDefault();
        }

        public bool UserFileInfoExist(string name)
        {
            return _context.UserFileInfo.Any(p => p.Name == name);
        }

        public bool CreateUserFileInfo(UserFileInfo info)
        {
            _context.UserFileInfo.Add(info);
            return Save();
        }

        public bool UpdateUserFileInfo(UserFileInfo info)
        {
            _context.Update(info);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
