using Application.Interfaces;
using File_sending.Services;
using Moq;

namespace File_Sending_Tests
{
    public class WorkerSalaryCalculationUnitTests
    {
        [Fact]
        public void CalculateSalary_WhenHourPayAndWorkedHoursWeeekly_ReturnSalary()
        {
            //Arrange
            var hourPay = 30.50;
            var workedHoursWeekly = 40;
            var expectedCalculatedSalary = (hourPay * workedHoursWeekly) * 52;

            //Act
            var _service = new SalaryCalculatorService();
            var calculatedSalary = _service.CalculateYearlySalary(hourPay, workedHoursWeekly);

            //Assert
            Assert.True(expectedCalculatedSalary == calculatedSalary);
        }

        [Fact]
        public void CalculateSalary_WhenHourPayMockedAndWorkedHoursWeeekly_ReturnSalary() 
        {
            //Arrange
            var workedHoursWeekly = 40;
            var expectedCalculatedSalary = 400*52;
            var hoursServiceMock = new Mock<IHourPayService>();
            hoursServiceMock.Setup(x => x.GetHourPay()).Returns(10.0);

            //Act
            var _service = new SalaryCalculatorService();
            var calculatedSalary = _service.CalculateYearlySalaryWithExternalDependency(hoursServiceMock.Object, workedHoursWeekly);

            //Assert
            Assert.True(expectedCalculatedSalary == calculatedSalary);
        }
    }
}
