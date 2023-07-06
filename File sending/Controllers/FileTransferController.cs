using File_sending.Interfaces;
using File_sending.Models;
using File_sending.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace File_sending.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileTransferController : ControllerBase
    {
        private readonly ICSVHelperService _csvService;
        private readonly IUserFileInfoRepository _UserFileInfoRepository;

        public FileTransferController(ICSVHelperService csvService, IUserFileInfoRepository UserFileInfoRepository)
        {
            _csvService = csvService;
            _UserFileInfoRepository = UserFileInfoRepository;
        }

        [HttpPost(Name = "PostFile")]
        public async Task<IActionResult> PostUserFile(IFormFile file)
        {
            var filename = file.FileName.Substring(0, file.FileName.LastIndexOf('.'));
            var data = _csvService.ReadCSV<UserFile>(file.OpenReadStream());
            var json = JsonSerializer.Serialize(data);

            if (_UserFileInfoRepository.UserFileInfoExist(filename))
            {
                var oldEntity = _UserFileInfoRepository.GetUserFileInfo(filename);
                oldEntity.Name = filename;
                oldEntity.Content = json;
                oldEntity.Updated = DateTime.UtcNow;
                _UserFileInfoRepository.UpdateUserFileInfo(oldEntity);
                return Ok(oldEntity);
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
                _UserFileInfoRepository.CreateUserFileInfo(entity);
                return Ok(entity);
            }
        }

        //[HttpGet(Name = "GetFilesInfo")]
        //public IActionResult GetAllUserFileInfo()
        //{
        //    var data = _UserFileInfoRepository.GetAll();
        //    return Ok(data);
        //}

        [HttpGet(Name = "GetFileByName")]
        public async Task<IActionResult> GetUserFileInfoContent(string name)
        {
            if (!_UserFileInfoRepository.UserFileInfoExist(name))
                return BadRequest("No info about file with such name");
            var data = _UserFileInfoRepository.GetUserFileInfo(name);
            var deserealized = JsonSerializer.Deserialize<List<UserFile>>(data.Content);
            var csv = _csvService.WriteCSV<UserFile>(deserealized);

            return File(Encoding.UTF8.GetBytes(csv.ToString()), "text/csv", data.Name);
        }



    }
}
