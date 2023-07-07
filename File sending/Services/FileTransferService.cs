using File_sending.Interfaces;
using File_sending.Models;
using File_sending.Repository;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace File_sending.Services
{
    public class FileTransferService : ControllerBase
    {
        private readonly ICSVHelperService _csvService;
        private readonly IUserFileInfoRepository _UserFileInfoRepository;
        public FileTransferService(ICSVHelperService csvService, IUserFileInfoRepository UserFileInfoRepository)
        {
            _csvService = csvService;
            _UserFileInfoRepository = UserFileInfoRepository;
        }

        public async Task<UserFileInfo> UploadFile(IFormFile file)
        {
            var filename = file.FileName.Substring(0, file.FileName.LastIndexOf('.'));
            var data = _csvService.ReadCSV<UserFile>(file.OpenReadStream());
            var json = JsonSerializer.Serialize(data);

            if ( await _UserFileInfoRepository.UserFileInfoExist(filename))
            {
                var oldEntity = await _UserFileInfoRepository.GetUserFileInfo(filename);
                oldEntity.Name = filename;
                oldEntity.Content = json;
                oldEntity.Updated = DateTime.UtcNow;
                await _UserFileInfoRepository.UpdateUserFileInfo(oldEntity);
                return oldEntity;
            }
            else
            {
                var entity = new UserFileInfo
                {
                    Name = file.FileName.Substring(0, file.FileName.LastIndexOf('.')),
                    Content = json,
                    Type = file.FileName.Substring(file.FileName.LastIndexOf('.') + 1),
                    Created = DateTime.UtcNow,
                    Updated = DateTime.UtcNow

                };
                await _UserFileInfoRepository.CreateUserFileInfo(entity);
                return entity;
            }
        }

        public async Task<bool> IsExist(string name)
        {
            return await _UserFileInfoRepository.UserFileInfoExist(name);
        }

        public async Task<FileContentResult> DownloadFile(string name)
        {
            var data = await _UserFileInfoRepository.GetUserFileInfo(name);
            var deserealized = JsonSerializer.Deserialize<List<UserFile>>(data.Content);
            var csv = _csvService.WriteCSV<UserFile>(deserealized);

            return File(Encoding.UTF8.GetBytes(csv.ToString()), "text/csv", data.Name);
        }
    }
}
