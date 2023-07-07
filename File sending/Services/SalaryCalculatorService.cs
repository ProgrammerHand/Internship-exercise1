namespace File_sending.Services
{
    public class SalaryCalculatorService
    {
        public double CalculateSalary(double hourPay, int workedHoursWeekly)
        {
            return (hourPay * workedHoursWeekly) *52;
        }
    }
}
