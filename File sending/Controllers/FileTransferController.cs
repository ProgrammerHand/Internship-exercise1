using File_sending.Services;
using Microsoft.AspNetCore.Mvc;

namespace File_sending.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileTransferController : ControllerBase
    {
        private readonly FileTransferService _trasferService;

        public FileTransferController(FileTransferService trasferService)
        {
            _trasferService = trasferService;
        }

        [HttpPost(Name = "PostFile")]
        public async Task<IActionResult> PostUserFile(IFormFile file)
        {
            return Ok( await _trasferService.UploadFile(file));
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
            if (!await _trasferService.IsExist(name))
                return BadRequest("No info about file with such name");
            return Ok(await _trasferService.DownloadFile(name));
        }
    }
}
