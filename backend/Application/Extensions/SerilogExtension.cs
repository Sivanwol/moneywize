using Application.Utils;
using Microsoft.Extensions.Hosting;
using Serilog;
namespace Application.Extensions;

public static class SerilogExtension {

    public static void ProcessLogHandler(string applicationName, string env = "development") {
        Log.Logger = ServicesLogger.GetLogger(applicationName);
        Log.Information($"Started Logger System Work on Environment: {env}");
    }
    public static IHostBuilder UseSerilogExtenstion(this IHostBuilder builder) {
        builder.UseSerilog(Log.Logger, true);
        return builder;
    }
}