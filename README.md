# DotNetApiLogging
A .NET API logging library. To add this to your project add code like the following to your dependency injection setup:

```
builder.AddLoggerDependencies([
    new DotNetApiLogging.Di.DependencyInjector.JsonFileConfig("appsettings.json", optional: false),
    new DotNetApiLogging.Di.DependencyInjector.JsonFileConfig("appsettings.Development.json", optional: true),
    ]);
```

The initializes the logging infrastructure using the `appsettings.json` file and optionally a `appsettings.Development.json` file(s). The API logging library uses [serilog](https://serilog.net/)