using Application.Interfaces;

namespace File_sending.Services
{
    public class SalaryCalculatorService
    {
        public double CalculateSalary(double hourPay, int workedHoursWeekly)
        {
            return (hourPay * workedHoursWeekly) *52;
        }

        public double CalculateSalaryWithExternalDependency(IHourPayService _hourPayService, int workedHoursWeekly)
        {
            return (_hourPayService.GetHourPay() * workedHoursWeekly) * 52;
        }
    }
}
