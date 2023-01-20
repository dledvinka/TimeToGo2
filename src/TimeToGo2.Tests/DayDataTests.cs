namespace TimeToGo2.Tests
{
    using FluentAssertions;
    using FluentAssertions.Execution;
    using NUnit.Framework;
    using TimeToGo2.WebApp.Data;
    using TimeToGo2.WebApp.ViewModels;

    public class DayViewModelTests
    {
        private readonly JobConstraints jobConstraints = new();

        [TestCase(8, 0, 9, 0, null, -420)]
        [TestCase(8, 0, 14, 0, null, -120)]
        [TestCase(8, 0, 14, 15, null, -120)]
        [TestCase(8, 0, 14, 30, null, -120)]
        [TestCase(8, 0, 14, 31, null, -119)]
        [TestCase(8, 0, 18, 31, null, 121)]
        [TestCase(8, 0, 14, 0, 20, -140)]
        [TestCase(8, 0, 14, 0, 10, -130)]
        [TestCase(8, 0, 14, 0, 0, -120)]
        [TestCase(8, 0, 14, 30, 20, -120)]
        [TestCase(8, 0, 14, 30, 60, -150)]
        public void ManuallyEnteredDataTest(int timeArrivedHour, int timeArrivedMinute, int timeLeftHour, int timeLeftMinute,
                                   int? pauseDurationInMinutes, int expectedDailyDelta)
        {
            var dayViewModel = new DayViewModel(jobConstraints)
            {
                TimeArrived = TimeOnly.FromTimeSpan(TimeSpan.FromHours(timeArrivedHour) + TimeSpan.FromMinutes(timeArrivedMinute)),
                TimeLeft = TimeOnly.FromTimeSpan(TimeSpan.FromHours(timeLeftHour) + TimeSpan.FromMinutes(timeLeftMinute)),
                SpentOutside = pauseDurationInMinutes.HasValue ? TimeSpan.FromMinutes(pauseDurationInMinutes.Value) : TimeSpan.Zero,
            };

            dayViewModel.IsWorkDay = true;

            using (new AssertionScope())
            {
                dayViewModel.IsValid.Should().BeTrue();
                dayViewModel.DailyDelta.TotalMinutes.Should().Be(expectedDailyDelta);
            }
        }

        [TestCase(-15, -15)]
        [TestCase(0, 0)]
        [TestCase(15, 15)]
        public void GateDataTest(int gateSystemDeltaInMinutes, int expectedDailyDeltaInMinutes)
        {
            var dayViewModel = new DayViewModel(jobConstraints)
            {
                DeltaFromGateSystem = TimeSpan.FromMinutes(gateSystemDeltaInMinutes),
                IsWorkDay = true
            };

            using (new AssertionScope())
            {
                dayViewModel.IsValid.Should().BeTrue();
                dayViewModel.DailyDelta.TotalMinutes.Should().Be(expectedDailyDeltaInMinutes);
            }
        }
    }
}