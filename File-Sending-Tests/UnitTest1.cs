using File_sending.Controllers;
using File_sending.Interfaces;
using File_sending.Models;
using File_sending.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace File_Sending_Tests
{
    [TestClass]
    public class UnitTest1
    {
        //private readonly ICSVHelperService csvService;
        //private readonly IUserFileInfoRepository UserFileInfoRepository;

        //[Fact]
        //public void UploadUserFile_ShoudReturnEntity()
        //{
        //    var _controller = new FileTransferController(csvService, UserFileInfoRepository);
        //    //var stream = System.IO.File.OpenRead("C:\\Users\\dprotsailo\\Documents\\test1.csv");
        //    //var file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
        //    Assert.IsInstanceOfType(_controller.PostUserFile(myfile).GetType(), typeof(UserFileInfo));
        //}

        [Fact]
        public void dummyTest()
        {
            Assert.IsTrue(true);
        }
    }
}