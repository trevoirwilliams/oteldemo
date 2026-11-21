# Instrument an ASP.NET Core API

This is the weather and order-processing API for lectures 07–14.

| Branch | When to use it |
| --- | --- |
| [`start`](https://github.com/trevoirwilliams/oteldemo/tree/start) | Run the sample before adding telemetry. |
| [`practice`](https://github.com/trevoirwilliams/oteldemo/tree/practice) | Work through the lesson-numbered TODOs. |
| [`final`](https://github.com/trevoirwilliams/oteldemo/tree/final) | Run the completed lab or compare it with your work. |

Install the current stable [.NET 10 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/10.0) and run `dotnet --version` from the repo. Use `10.0.400` or a later stable .NET 10 SDK. Any C# editor works. The `.http` files are ready for an editor with an HTTP client extension. Docker with Compose is needed starting in lecture 13.

## Run the API

```sh
dotnet run --launch-profile http
```

Send `GET http://127.0.0.1:5236/weatherforecast` or open `OTelDemo.http`.

## Follow the videos

- **09** Open `OTelDemo.csproj` and `Program.cs`. Add stable OpenTelemetry Hosting, Console, OTLP, ASP.NET Core and HTTP packages at `1.21.0`. Wire up console tracing, send the weather request, and find the span and trace ID.
- **10–11** Add the custom endpoint, activity source, meter and counter. Send the two order IDs in `OTelDemo.http` and check that the counter has one series. Add `OpenTelemetry.Instrumentation.Runtime` at `1.21.0`, enable runtime collection and look for `dotnet.thread_pool.thread.count` in the console metrics. Allow one export interval. To shorten it, set `OTEL_METRIC_EXPORT_INTERVAL` to `5000` before starting the API.
- **13–14** Open `docker-compose.yml` and complete its TODO, then follow the [backend setup](#backend-setup-for-lectures-13-and-14) below. The reference implementation is on `final`.

## Backend setup for lectures 13 and 14

The videos show a prerelease exporter and a scrape job for the API. Use these steps with the stable packages in this repo.

1. In `docker-compose.yml`, enable the Prometheus receiver with `--web.enable-otlp-receiver`. `prometheus.yml` keeps the classic metric names used in the lessons.
2. In `Program.cs`, send traces to Jaeger using `AddOtlpExporter`, gRPC and `http://127.0.0.1:4317`.
3. Use the metrics version of `AddOtlpExporter` with `OtlpExportProtocol.HttpProtobuf` and `http://127.0.0.1:9090/api/v1/otlp/v1/metrics`. Set the periodic export interval to 5000 milliseconds for the lab. Skip the application scrape target, `AddPrometheusExporter` and `MapPrometheusScrapingEndpoint` shown in the recording.
4. Run `docker compose up -d`, restart the API and send the weather request and both POST requests in `OTelDemo.http`.
5. Open [Jaeger](http://127.0.0.1:16686), select `OTelDemo` and find the span named `ProcessOrder` in one of the POST traces.
6. Open [Prometheus](http://127.0.0.1:9090) and run `http_server_request_duration_seconds_count{job="OTelDemo"}` and `OrdersProcessed_total{job="OTelDemo"}`. Give the API at least one export interval. The order counter has one series for the two IDs in that run. Use the query view for app metrics because the Targets page describes pull-based scraping.

Run `docker compose down` before starting the checkout lab. The two repositories use the same Jaeger and Prometheus ports.

See [the course index](https://github.com/trevoirwilliams/oteldemo#checkpoints) for intermediate lesson checkpoints. To compare without changing your working branch, open the checkpoint on GitHub.
