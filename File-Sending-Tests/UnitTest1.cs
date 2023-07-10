using File_sending.Data;
using File_sending.Interfaces;
using File_sending.Repository;
using File_sending.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection.PortableExecutable;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace File_Sending_Tests
{
    [TestClass]
    public class UnitTest1
    {

        [Fact]
        public async Task GivenCVVFile_WhenNotInDatabase_ShouldBeAddedToDatabase()
        {
            //Arrange
            var csvHelperService = new CSVHelperService();
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new AppDbContext(optionsBuilder.Options);
            var repository = new FileTransferRepository(context);
            var service = new FileTransferService(csvHelperService, repository);

            //Act
            var stream = System.IO.File.OpenRead("test2.csv");
            IFormFile file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
            service.UploadFile(file);
            var result = await repository.UserFileInfoExist(file.FileName.Substring(0, file.FileName.LastIndexOf('.')));
            

            //Assert
            Assert.IsTrue(result);
        }

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