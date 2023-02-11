namespace TimeToGo2.WebApp.ViewModels;

using MediatR;
using TimeToGo2.WebApp.Configuration;
using TimeToGo2.WebApp.Features.Month;

public class MonthPageViewModel
{
    private readonly IJobConstraints _jobConstraints;
    private readonly IMediator _mediator;

    public MonthDataViewModel MonthData { get; set; }

    public MonthPageViewModel(IMediator mediator, IJobConstraints jobConstraints)
    {
        _mediator = mediator;
        _jobConstraints = jobConstraints;
    }

    public async Task InitializeAsync(string? userIdentityName, int year, int month)
    {
        var monthData = await _mediator.Send(new GetList.Query(userIdentityName, year, month));
        MonthData = new MonthDataViewModel(_jobConstraints);
        MonthData.SetData(monthData);
    }

    public async Task UpdateDayAsync(DayViewModel dayViewModel)
    {
        if (dayViewModel.IsValid)
        {
            var updateResult = await _mediator.Send(new UpdateDay.Command(dayViewModel.GetData()));
        }
    }
}