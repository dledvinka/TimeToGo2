namespace TimeToGo2.Tests
{
    using FluentAssertions;
    using NUnit.Framework;
    using TimeToGo2.WebApp.Data;

    public class DayDataTests
    {
        private readonly JobConstraints jobConstraints = new ();

        [Test]
        public void BeenAtWorkOneHourTest()
        {
            var dayData = new DayData(1, jobConstraints)
            {
                TimeArrived = TimeOnly.FromTimeSpan(TimeSpan.FromHours(8)),
                TimeLeft = TimeOnly.FromTimeSpan(TimeSpan.FromHours(9))
            };

            dayData.WorkedHours.Should().Be(TimeSpan.FromHours(1));
            dayData.CalculatedPauseDuration.Should().Be(TimeSpan.FromMinutes(0));
        }

        [Test]
        public void BeenAtWorkSixHoursTest()
        {
            var dayData = new DayData(1, jobConstraints)
            {
                TimeArrived = TimeOnly.FromTimeSpan(TimeSpan.FromHours(8)),
                TimeLeft = TimeOnly.FromTimeSpan(TimeSpan.FromHours(14))
            };

            dayData.WorkedHours.Should().Be(TimeSpan.FromHours(6));
            dayData.CalculatedPauseDuration.Should().Be(TimeSpan.FromMinutes(0));
        }

        [Test]
        public void BeenAtWorkSixHoursAndFifteenMinutesTest()
        {
            var dayData = new DayData(1, jobConstraints)
            {
                TimeArrived = TimeOnly.FromTimeSpan(TimeSpan.FromHours(8)),
                TimeLeft = TimeOnly.FromTimeSpan(TimeSpan.FromHours(14) + TimeSpan.FromMinutes(15))
            };

            dayData.WorkedHours.Should().Be(TimeSpan.FromHours(6));
            dayData.CalculatedPauseDuration.Should().Be(TimeSpan.FromMinutes(15));
        }

        [Test]
        public void BeenAtWorkSixHoursAndThirtyMinutesTest()
        {
            var dayData = new DayData(1, jobConstraints)
            {
                TimeArrived = TimeOnly.FromTimeSpan(TimeSpan.FromHours(8)),
                TimeLeft = TimeOnly.FromTimeSpan(TimeSpan.FromHours(14) + TimeSpan.FromMinutes(30))
            };

            dayData.WorkedHours.Should().Be(TimeSpan.FromHours(6));
            dayData.CalculatedPauseDuration.Should().Be(TimeSpan.FromMinutes(30));
        }

        [Test]
        public void BeenAtWorkSixHoursAndThirtyOneMinutesTest()
        {
            var dayData = new DayData(1, jobConstraints)
            {
                TimeArrived = TimeOnly.FromTimeSpan(TimeSpan.FromHours(8)),
                TimeLeft = TimeOnly.FromTimeSpan(TimeSpan.FromHours(14) + TimeSpan.FromMinutes(31))
            };

            dayData.WorkedHours.Should().Be(TimeSpan.FromHours(6) + TimeSpan.FromMinutes(1));
            dayData.CalculatedPauseDuration.Should().Be(TimeSpan.FromMinutes(30));
        }

        [Test]
        public void BeenAtWorkTenHoursAndThirtyOneMinutesTest()
        {
            var dayData = new DayData(1, jobConstraints)
            {
                TimeArrived = TimeOnly.FromTimeSpan(TimeSpan.FromHours(8)),
                TimeLeft = TimeOnly.FromTimeSpan(TimeSpan.FromHours(18) + TimeSpan.FromMinutes(31))
            };

            dayData.WorkedHours.Should().Be(TimeSpan.FromHours(10) + TimeSpan.FromMinutes(1));
            dayData.CalculatedPauseDuration.Should().Be(TimeSpan.FromMinutes(30));
        }
    }
}