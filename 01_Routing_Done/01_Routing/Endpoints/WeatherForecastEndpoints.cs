namespace _01_Routing.Endpoints
{
    public static class WeatherForecastEndpoints
    {
        public static RouteGroupBuilder MapWeatherForecastApi(this RouteGroupBuilder group)
        {
            List<WeatherForecast> forecasts = Enumerable.Range(1, 10).Select(index => new WeatherForecast
            (
                index,
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 55)
            )).ToList();

            group.MapGet("/{id:int}", (int id) =>
            {
                return forecasts.FirstOrDefault(f => f.index == id) is WeatherForecast forecast
                    ? Results.Ok(forecast)
                    : Results.NotFound();
            });

            group.MapGet("/{username}", (string username) =>
            {
                return Results.Ok($"Hello {username}!");
            });

            group.MapGet("/", (int page) =>
            {
                if (page < 1)
                {
                    return Results.BadRequest("Page number must be greater than 0.");
                }

                return Results.Json(forecasts.Skip((page - 1) * 5).Take(5).ToList());
            });

            group.MapPost("/", (WeatherForecastCreateDto forecast) =>
            {
                // assumes that indexes handled like in SQL (auto increment)
                int lastIndex = forecasts[forecasts.Count - 1].index;

                forecasts.Add(new WeatherForecast(lastIndex + 1, forecast.Date, forecast.TemperatureC));

                return Results.Ok();
            });

            return group;
        }
    }
}
