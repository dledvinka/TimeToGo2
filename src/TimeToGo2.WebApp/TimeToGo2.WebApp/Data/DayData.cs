using TimeToGo2.WebApp.Data;

public class DayData
{
    private readonly JobConstraints jobConstraints;

    public TimeSpan CalculatedPauseDuration
    {
        get
        {
            var pauseDuration = PauseDuration ?? jobConstraints.MinimalLunchPauseDuration;
            var timeLeft = TimeLeft?.ToTimeSpan() ?? (TimeArrived.ToTimeSpan() + TimeSpan.FromHours(8));
            var workedDuration = timeLeft - TimeArrived.ToTimeSpan();
            var workedOverDailyLunchPa =
                TimeSpan.FromMinutes(Math.Max((workedDuration - jobConstraints.LunchPauseAppliesAfterAmountOfWorkedHours).TotalMinutes, 0));
            var calculatedPauseDuration = TimeSpan.FromMinutes(PauseDuration.HasValue
                                                                   ? Math.Max(workedOverDailyLunchPa.TotalMinutes, pauseDuration.TotalMinutes)
                                                                   : Math.Min(workedOverDailyLunchPa.TotalMinutes, pauseDuration.TotalMinutes));

            return calculatedPauseDuration;
        }
    }

    public int DayInMonth { get; }

    public TimeSpan? PauseDuration { get; set; }

    public TimeOnly TimeArrived { get; set; }

    public TimeOnly? TimeLeft { get; set; }

    public TimeSpan WorkedHours => GetWorkedHours();

    public DayOfWeek DayOfWeek { get; }

    public DayData(int year, int month, int dayInMonth, JobConstraints jobConstraints)
    {
        this.jobConstraints = jobConstraints;
        DayInMonth = dayInMonth;
        DayOfWeek = new DateTime(year, month, dayInMonth).DayOfWeek;
    }

    private TimeSpan GetWorkedHours()
    {
        var timeLeft = TimeLeft?.ToTimeSpan() ?? (TimeArrived.ToTimeSpan() + TimeSpan.FromHours(8));
        var workedDuration = timeLeft - TimeArrived.ToTimeSpan();

        return workedDuration - CalculatedPauseDuration;
    }
}