namespace TimeToGo2.Tests
{
    using FluentAssertions;
    using FluentAssertions.Execution;
    using NUnit.Framework;
    using TimeToGo2.WebApp.Data;

    public class DayDataTests
    {
        private readonly JobConstraints jobConstraints = new();

        [TestCase(8, 0, 9, 0, null, 60, 0)]
        [TestCase(8, 0, 14, 0, null, 360, 0)]
        [TestCase(8, 0, 14, 15, null, 360, 15)]
        [TestCase(8, 0, 14, 30, null, 360, 30)]
        [TestCase(8, 0, 14, 31, null, 361, 30)]
        [TestCase(8, 0, 18, 31, null, 601, 30)]
        [TestCase(8, 0, 14, 0, 20, 340, 20)]
        [TestCase(8, 0, 14, 0, 10, 350, 10)]
        [TestCase(8, 0, 14, 0, 0, 360, 0)]
        [TestCase(8, 0, 14, 30, 20, 360, 30)]
        [TestCase(8, 0, 14, 30, 60, 330, 60)]
        public void BeenAtWorkTest(int timeArrivedHour, int timeArrivedMinute, int timeLeftHour, int timeLeftMinute,
                                   int? pauseDurationInMinutes, int expectedWorkedHoursInMinutes, int expectedCalculatedPauseDuration)
        {
            var dayData = new DayData(1, jobConstraints)
            {
                TimeArrived = TimeOnly.FromTimeSpan(TimeSpan.FromHours(timeArrivedHour) + TimeSpan.FromMinutes(timeArrivedMinute)),
                TimeLeft = TimeOnly.FromTimeSpan(TimeSpan.FromHours(timeLeftHour) + TimeSpan.FromMinutes(timeLeftMinute)),
                PauseDuration = pauseDurationInMinutes.HasValue ? TimeSpan.FromMinutes(pauseDurationInMinutes.Value) : null
            };

            using (new AssertionScope())
            {
                dayData.WorkedHours.TotalMinutes.Should().Be(expectedWorkedHoursInMinutes);
                dayData.CalculatedPauseDuration.Should().Be(TimeSpan.FromMinutes(expectedCalculatedPauseDuration));
            }
        }
    }
}