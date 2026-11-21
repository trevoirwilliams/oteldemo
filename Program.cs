var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();

// Lecture 09: Register OpenTelemetry, name the resource WeatherForecast API, and export HTTP traces to the console.
// Send the weather request and find its server span before you add custom instrumentation.

// Lecture 10: Rename the service to OTelDemo. Declare an ActivitySource, a Meter and an OrdersProcessed counter.
// Subscribe to the source and meter. Add server and HTTP client metrics and export them to the console.

// Lecture 11: Enable HTTP exception recording and runtime metrics. Look for a runtime metric separately.

// Lecture 14: Send traces to Jaeger over OTLP/gRPC. Replace console metrics with OTLP/HTTP for Prometheus.
// Use the endpoints in the README and check each signal in its own UI.

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

string[] summaries =
[
    "Freezing", "Bracing", "Chilly", "Cool", "Mild",
    "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
];

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5)
        .Select(day => new WeatherForecast(
            DateOnly.FromDateTime(DateTime.Now.AddDays(day)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]))
        .ToArray();

    return forecast;
})
.WithName("GetWeatherForecast");

// Lecture 10: Add POST /orders/{orderId:int}/process here. Start a ProcessOrder activity,
// put order.id and cart.size on the span, and increment the counter once per successful request.

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
