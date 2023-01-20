namespace TimeToGo2.WebApp.Data;

public interface IJobConstraints
{
    TimeSpan MinimalLunchPauseDuration { get; }
    TimeSpan LunchPauseAppliesAfterAmountOfWorkedHours { get; }
    TimeSpan WorkHoursPerDay { get; }
}