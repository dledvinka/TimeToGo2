using TimeToGo2.WebApp.Data;

public class MonthService : IMonthService
{
    public Task<MonthData> GetAsync(int year, int month)
    {
        var monthData = new MonthData(year, month);

        var daysInMonth = DateTime.DaysInMonth(year, month);
        var jobConstraints = new JobConstraints();

        monthData.Days = Enumerable.Range(1, daysInMonth).Select(day => new DayData(year, month, day, jobConstraints)
        {
            TimeArrived = new TimeOnly(8, 0),
            TimeLeft = new TimeOnly(16, 30)
        }).ToList();

        return Task.FromResult(monthData);
    }
}