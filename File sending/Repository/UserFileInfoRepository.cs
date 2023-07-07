using File_sending.Data;
using File_sending.Interfaces;
using File_sending.Models;
using Microsoft.EntityFrameworkCore;

namespace File_sending.Repository
{
    public class UserFileInfoRepository : IUserFileInfoRepository
    {
        private readonly AppDbContext _context;

        public UserFileInfoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<UserFileInfo>> GetAll() 
        {
            return await _context.UserFileInfo.OrderBy(p => p.Id).ToListAsync();
        }

        public async Task<UserFileInfo> GetUserFileInfo(int id)
        {
            return await _context.UserFileInfo.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<UserFileInfo> GetUserFileInfo(string name)
        {
            return await _context.UserFileInfo.FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task<bool> UserFileInfoExist(string name)
        {
            return await _context.UserFileInfo.AnyAsync(p => p.Name == name);
        }

        public async Task<bool> CreateUserFileInfo(UserFileInfo info)
        {
            await _context.UserFileInfo.AddAsync(info);
            return  await Save();
        }

        public async Task<bool> UpdateUserFileInfo(UserFileInfo info)
        {
            _context.Update(info);
            return await Save();
        }

        public async Task<bool> Save()
        {
            var saved = _context.SaveChangesAsync();
            return await saved > 0 ? true : false;
        }
    }
}
