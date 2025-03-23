# DotNetApiLogging
A .NET API logging library. To add this to your project add code like the following to your dependency injection setup:

```
var Config = new LogConfig()
{
    Path = options.LogPath,
    MinimumLogLevel = options.LogLevel
};
appBuilder.AddLoggerDependencies(Config);
```

The initializes the logging infrastructure using the `appsettings.json` file and optionally a `appsettings.Development.json` file(s). The API logging library uses [serilog](https://serilog.net/)