public class MonthService : IMonthService
{
    public Task<MonthData> GetAsync(int year, int month)
    {
        var monthData = new MonthData();
        monthData.Year = year;
        monthData.Month = month;

        var daysInMonth = DateTime.DaysInMonth(year, month);

        monthData.Days = Enumerable.Range(1, daysInMonth).Select(day => new DayData()
        {
            DayInMonth = day,
            TimeArrived = new TimeOnly(8, 0),
            TimeLeft = new TimeOnly(16, 30),
            IsWorkDay = new DateOnly(year, month, day).DayOfWeek is not (DayOfWeek.Saturday or DayOfWeek.Sunday)
        }).ToList();

        return Task.FromResult(monthData);
    }
}