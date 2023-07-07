using File_sending.Interfaces;
using File_sending.Models;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace File_sending.Services
{
    public class FileTransferService
    {
        private readonly ICSVHelperService _csvService;
        private readonly Interfaces.IFileTransferRepository _fileTransferRepository;
        public FileTransferService(ICSVHelperService csvService, Interfaces.IFileTransferRepository FileTransferRepository)
        {
            _csvService = csvService;
            _fileTransferRepository = FileTransferRepository;
        }

        public async Task<FileSpecs> UploadFile(IFormFile file)
        {
            var filename = file.FileName.Substring(0, file.FileName.LastIndexOf('.'));
            var data = _csvService.ReadCSV<FileContentStructure>(file.OpenReadStream());
            var json = JsonSerializer.Serialize(data);

            if ( await _fileTransferRepository.UserFileInfoExist(filename))
            {
                var oldEntity = await _fileTransferRepository.GetUserFileInfo(filename);
                oldEntity.Name = filename;
                oldEntity.Content = json;
                oldEntity.Updated = DateTime.UtcNow;
                await _fileTransferRepository.UpdateUserFileInfo(oldEntity);
                return oldEntity;
            }
            else
            {
                var entity = new FileSpecs
                {
                    Name = file.FileName.Substring(0, file.FileName.LastIndexOf('.')),
                    Content = json,
                    Type = file.FileName.Substring(file.FileName.LastIndexOf('.') + 1),
                    Created = DateTime.UtcNow,
                    Updated = DateTime.UtcNow

                };
                await _fileTransferRepository.CreateUserFileInfo(entity);
                return entity;
            }
        }

        public async Task<bool> IsExist(string name)
        {
            return await _fileTransferRepository.UserFileInfoExist(name);
        }

        public async Task<(string data,string name)> DownloadFile(string name)
        {
            var data = await _fileTransferRepository.GetUserFileInfo(name);
            var deserealized = JsonSerializer.Deserialize<List<FileContentStructure>>(data.Content);
            var csv = _csvService.WriteCSV<FileContentStructure>(deserealized);

            return (csv.ToString(), data.Name);
        }
    }
}
