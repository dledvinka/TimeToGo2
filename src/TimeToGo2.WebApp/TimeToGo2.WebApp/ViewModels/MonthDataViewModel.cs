namespace TimeToGo2.WebApp.ViewModels
{
    using TimeToGo2.WebApp.Configuration;

    public class MonthDataViewModel
    {
        private readonly IJobConstraints _jobConstraints;
        public List<DayViewModel> Days { get; private set; }
        public int Month { get; private set; }
        public int Year { get; private set; }

        public MonthDataViewModel(IJobConstraints jobConstraints)
        {
            _jobConstraints = jobConstraints;
        }

        public void SetData(MonthData data)
        {
            Year = data.Year;
            Month = data.Month;
            Days = data.Days.Select(dayData =>
            {
                var dayViewModel = new DayViewModel(_jobConstraints);
                dayViewModel.SetData(data.Year, data.Month, dayData);

                return dayViewModel;
            }).ToList();
        }

        public MonthData GetData() =>
            new();
    }
}