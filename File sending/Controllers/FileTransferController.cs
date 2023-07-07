using File_sending.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace File_sending.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileTransferController : ControllerBase
    {
        private readonly FileTransferService _trasferService;

        public FileTransferController(FileTransferService trasferService)
        {
            _trasferService = trasferService;
        }

        [HttpPost("UploadFile")]
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

        [HttpGet("DownloadFile/{name}")]
        public async Task<IActionResult> GetUserFileInfoContent(string name)
        {
            if (!await _trasferService.IsExist(name))
                return BadRequest("No info about file with such name");
            var result = await _trasferService.DownloadFile(name);
            return File(Encoding.UTF8.GetBytes(result.data.ToString()), "text/csv", result.name);
        }
    }
}
