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

Send `GET http://127.0.0.1:5236/weatherforecast` or open `OTelDemo.http`. Look for the weather request in the console.

See [the course index](https://github.com/trevoirwilliams/oteldemo#checkpoints) for intermediate lesson checkpoints. To compare without changing your working branch, open the checkpoint on GitHub.
