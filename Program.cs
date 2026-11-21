using System.Diagnostics;
using System.Diagnostics.Metrics;
using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();

const string activitySourceName = "OTelDemo.Activities";
const string meterName = "OTelDemo.Meter";
using var activitySource = new ActivitySource(activitySourceName);
using var meter = new Meter(meterName, "1.0.0");
var ordersProcessedCounter = meter.CreateCounter<long>("OrdersProcessed");

builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource.AddService("OTelDemo", serviceVersion: "1.0.0"))
    .WithTracing(tracing => tracing
        .AddAspNetCoreInstrumentation(options => options.RecordException = true)
        .AddHttpClientInstrumentation()
        .AddSource(activitySourceName)
        .AddOtlpExporter(options =>
        {
            options.Protocol = OtlpExportProtocol.Grpc;
            options.Endpoint = new Uri(builder.Configuration["Telemetry:TracesEndpoint"]
                ?? "http://127.0.0.1:4317");
        }))
    .WithMetrics(metrics => metrics
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddMeter(meterName)
        .AddRuntimeInstrumentation()
        .AddOtlpExporter((options, reader) =>
        {
            options.Protocol = OtlpExportProtocol.HttpProtobuf;
            options.Endpoint = new Uri(builder.Configuration["Telemetry:MetricsEndpoint"]
                ?? "http://127.0.0.1:9090/api/v1/otlp/v1/metrics");
            reader.PeriodicExportingMetricReaderOptions.ExportIntervalMilliseconds = 5000;
        }));

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

app.MapPost("/orders/{orderId:int}/process", (int orderId) =>
{
    using var activity = activitySource.StartActivity("ProcessOrder");
    var cartSize = Random.Shared.Next(1, 6);

    activity?.SetTag("order.id", orderId);
    activity?.SetTag("cart.size", cartSize);
    ordersProcessedCounter.Add(1);

    return Results.Ok(new
    {
        OrderId = orderId,
        Status = "Processed",
        CartSize = cartSize
    });
})
.WithName("ProcessOrder");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
