using TimeToGo2.WebApp.Data;

public class DayData
{
    private readonly JobConstraints jobConstraints;
    public int DayInMonth { get; }

    public DayData(int dayInMonth, JobConstraints jobConstraints)
    {
        this.jobConstraints = jobConstraints;
        DayInMonth = dayInMonth;
    }

    public TimeOnly TimeArrived { get; set; }

    public TimeSpan? PauseDuration { get; set; }

    public TimeSpan CalculatedPauseDuration
    {
        get
        {
            var pauseDuration = PauseDuration ?? jobConstraints.MinimalLunchPauseDuration;
            var timeLeft = TimeLeft?.ToTimeSpan() ?? (TimeArrived.ToTimeSpan() + TimeSpan.FromHours(8));
            var workedDuration = timeLeft - TimeArrived.ToTimeSpan();
            var workedOverDailyLunchPa = TimeSpan.FromMinutes(Math.Max((workedDuration - jobConstraints.LunchPauseAppliesAfterAmountOfWorkedHours).TotalMinutes, 0));

            var calculatedPauseDuration = TimeSpan.FromMinutes(Math.Min(workedOverDailyLunchPa.TotalMinutes, pauseDuration.TotalMinutes));

            return calculatedPauseDuration;
        } 
    }

    public TimeOnly? TimeLeft { get; set; }

    public TimeSpan WorkedHours => GetWorkedHours();

    private TimeSpan GetWorkedHours()
    {
        var timeLeft = TimeLeft?.ToTimeSpan() ?? (TimeArrived.ToTimeSpan() + TimeSpan.FromHours(8));
        var workedDuration = timeLeft - TimeArrived.ToTimeSpan();

        return workedDuration - CalculatedPauseDuration;
    }
}