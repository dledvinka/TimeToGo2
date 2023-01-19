public interface IMonthService
{
    Task<MonthData> GetAsync(int year, int month);
}