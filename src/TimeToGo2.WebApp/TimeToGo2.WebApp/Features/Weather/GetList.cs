namespace TimeToGo2.WebApp.Features.Weather
{
	using MediatR;

	public class GetList
	{
		public record Query(DateOnly StartDate) : IRequest<List<WeatherForecastModel>>;

		public class WeatherForecastModel
		{
			public DateOnly Date { get; set; }

			public string? Summary { get; set; }

			public int TemperatureC { get; set; }

			public int TemperatureF => 32 + (int) (TemperatureC / 0.5556);
		}

		public class QueryHandler : IRequestHandler<Query, List<WeatherForecastModel>>
		{
			private static readonly string[] Summaries = new[]
			{
				"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
			};

			public Task<List<WeatherForecastModel>> Handle(Query request, CancellationToken cancellationToken)
			{
				return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecastModel
				{
					Date = request.StartDate.AddDays(index),
					TemperatureC = Random.Shared.Next(-20, 55),
					Summary = Summaries[Random.Shared.Next(Summaries.Length)]
				}).ToList());
			}
		}
	}
}