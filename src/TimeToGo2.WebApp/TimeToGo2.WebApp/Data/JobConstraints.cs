namespace TimeToGo2.WebApp.Data
{
    public class JobConstraints
    {
        public TimeSpan MinimalLunchPauseDuration => TimeSpan.FromMinutes(30);

        public TimeSpan LunchPauseAppliesAfterAmountOfWorkedHours => TimeSpan.FromHours(6);
        public TimeSpan WorkHoursPerDay => TimeSpan.FromHours(8);
    }
}