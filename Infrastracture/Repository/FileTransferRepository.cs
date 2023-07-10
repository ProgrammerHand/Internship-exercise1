using File_sending.Data;
using Microsoft.EntityFrameworkCore;

namespace File_sending.Repository
{
    public class FileTransferRepository : Interfaces.IFileTransferRepository
    {
        private readonly AppDbContext _context;

        public FileTransferRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Models.FileSpecs>> GetAll() 
        {
            return await _context.UserFileInfo.OrderBy(p => p.Id).ToListAsync();
        }

        public async Task<Models.FileSpecs> GetUserFileInfo(int id)
        {
            return await _context.UserFileInfo.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Models.FileSpecs> GetUserFileInfo(string name)
        {
            return await _context.UserFileInfo.FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task<bool> UserFileInfoExist(string name)
        {
            return await _context.UserFileInfo.AnyAsync(p => p.Name == name);
        }

        public async Task<bool> CreateUserFileInfo(Models.FileSpecs info)
        {
            await _context.UserFileInfo.AddAsync(info);
            return  await Save();
        }

        public async Task<bool> UpdateUserFileInfo(Models.FileSpecs info)
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
