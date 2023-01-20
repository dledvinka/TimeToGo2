namespace TimeToGo2.WebApp.ViewModels;

using TimeToGo2.WebApp.Data;

public class MonthPageViewModel
{
    private readonly IJobConstraints _jobConstraints;
    private readonly IMonthService _monthService;

    public MonthDataViewModel MonthData { get; set; }

    public MonthPageViewModel(IMonthService monthService, IJobConstraints jobConstraints)
    {
        _monthService = monthService;
        _jobConstraints = jobConstraints;
    }


    public async Task InitializeAsync(int year, int month)
    {
        var monthData = await _monthService.GetAsync(year, month);
        MonthData = new MonthDataViewModel(_jobConstraints);
        MonthData.SetData(monthData);
    }
}