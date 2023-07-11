using Application.Interfaces;

namespace File_sending.Services
{
    public class SalaryCalculatorService
    {
        public double CalculateYearlySalary(double hourPay, int workedHoursWeekly)
        {
            return (hourPay * workedHoursWeekly) *52;
        }

        public double CalculateYearlySalaryWithExternalDependency(IHourPayService _hourPayService, int workedHoursWeekly)
        {
            return (_hourPayService.GetHourPay() * workedHoursWeekly) * 52;
        }
    }
}
