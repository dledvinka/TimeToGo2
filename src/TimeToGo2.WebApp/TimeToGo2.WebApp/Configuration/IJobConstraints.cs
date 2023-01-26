namespace TimeToGo2.WebApp.Configuration;

public interface IJobConstraints
{
    TimeSpan MinimalLunchPauseDuration { get; }
    TimeSpan LunchPauseAppliesAfterAmountOfWorkedHours { get; }
    TimeSpan WorkHoursPerDay { get; }
}