namespace TimeToGo2.WebApp.Features.Month
{
    using MediatR;
    using TimeToGo2.WebApp.ViewModels;

    public class UpdateDay
    {
        public record Command(DayData Day) : IRequest<bool>;

        public class QueryHandler : IRequestHandler<Command, bool>
        {
            public Task<bool> Handle(Command request, CancellationToken cancellationToken) => Task.FromResult(true);
        }
    }
}