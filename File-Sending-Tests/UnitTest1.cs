using File_sending.Services;
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
        public void GivenCalculatedSalary_WhenHourPayAndWorkedHoursWeeeklyInserted_returnDouble()
        {
            //Arrange
            var hourPay = 30.50;
            var workedHoursWeekly = 40;
            var calculatedSalary = (hourPay * workedHoursWeekly) * 52;

            //Act
            var _service = new SalaryCalculatorService();
            var actionResult = _service.CalculateSalary(hourPay, workedHoursWeekly);

            //Assert
            Assert.IsInstanceOfType<double>(actionResult);
            Assert.IsTrue(actionResult == calculatedSalary);
        }
    }
}