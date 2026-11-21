# Instrument an ASP.NET Core API

Use this repo for lectures 07–14 of OpenTelemetry for .NET Developers. You’ll instrument the weather API, add a custom order span and counter, and send data to Jaeger and Prometheus.

| Branch | When to use it |
| --- | --- |
| [`start`](https://github.com/trevoirwilliams/oteldemo/tree/start) | Run the sample before adding telemetry. |
| [`practice`](https://github.com/trevoirwilliams/oteldemo/tree/practice) | Work through the lesson-numbered TODOs. |
| [`final`](https://github.com/trevoirwilliams/oteldemo/tree/final) | Run the completed lab or compare it with your work. |

Install the current stable [.NET 10 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/10.0) and run `dotnet --version` from the repo. Use `10.0.400` or a later stable .NET 10 SDK. Any C# editor works. The `.http` files are ready for an editor with an HTTP client extension. Docker with Compose is needed starting in lecture 13.

```sh
git clone --branch practice https://github.com/trevoirwilliams/oteldemo.git
cd oteldemo
```

## Checkpoints

- [Lecture 09 · Console tracing](https://github.com/trevoirwilliams/oteldemo/tree/3467489cd8fb849a16650868897e2f7cb2b35669)
- [Lectures 10–11 · Custom and runtime metrics in the console](https://github.com/trevoirwilliams/oteldemo/tree/6b345e20d16aa28b06992c02667878eb7bcc4445)
- [Lectures 13–14 · Stable backend setup](https://github.com/trevoirwilliams/oteldemo/blob/final/README.md#backend-setup-for-lectures-13-and-14)

Continue with [the two-service checkout lab](https://github.com/trevoirwilliams/oteldemo.ecommerce) in lecture 15.
