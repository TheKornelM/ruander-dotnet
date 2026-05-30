var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "API v1");
    });
}

List<WeatherForecast> forecasts = Enumerable.Range(1, 10).Select(index => new WeatherForecast
(
    index,
    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
    Random.Shared.Next(-20, 55)
)).ToList();

app.MapGet("/weatherforecast/{id:int}", (int id) =>
{
    return forecasts.FirstOrDefault(f => f.index == id) is WeatherForecast forecast
        ? Results.Ok(forecast)
        : Results.NotFound();
});

app.MapGet("/weatherforecast/{username}", (string username) =>
{
    return Results.Ok($"Hello {username}!");
});

app.MapGet("/weatherforecast", (int page) =>
{
    if (page < 1)
    {
        return Results.BadRequest("Page number must be greater than 0.");
    }

    return Results.Json(forecasts.Skip((page - 1) * 5).Take(5).ToList());
});

app.MapPost("/weatherforecast", (WeatherForecastCreateDto forecast) =>
{
    // assumes that indexes handled like in SQL (auto increment)
    int lastIndex = forecasts[forecasts.Count - 1].index;

    forecasts.Add(new WeatherForecast(lastIndex + 1, forecast.Date, forecast.TemperatureC));

    return Results.Ok();
});

app.Run();

internal record WeatherForecast(int index, DateOnly Date, int TemperatureC)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

internal record WeatherForecastCreateDto(DateOnly Date, int TemperatureC)
{
}
