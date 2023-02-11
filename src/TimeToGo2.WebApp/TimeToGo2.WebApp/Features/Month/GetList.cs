namespace TimeToGo2.WebApp.Features.Month
{
    using MediatR;

    public class GetList
    {
        public record Query(string? UserIdentityName, int Year, int Month) : IRequest<MonthData>;

        public class QueryHandler : IRequestHandler<Query, MonthData>
        {
            public Task<MonthData> Handle(Query request, CancellationToken cancellationToken)
            {
                var daysInMonth = DateTime.DaysInMonth(request.Year, request.Month);

                var monthData = new MonthData
                {
                    Year = request.Year,
                    Month = request.Month,
                    Days = Enumerable.Range(1, daysInMonth).Select(day => new DayData()
                    {
                        Id = 100 + day,
                        DayInMonth = day,
                        TimeArrived = new TimeOnly(8, 0),
                        TimeLeft = new TimeOnly(16, 30),
                        IsWorkDay = new DateOnly(request.Year, request.Month, day).DayOfWeek is not (DayOfWeek.Saturday or DayOfWeek.Sunday)
                    }).ToList()
                };

                return Task.FromResult(monthData);
            }
        }
    }
}
