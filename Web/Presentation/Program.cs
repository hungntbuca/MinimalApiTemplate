using Serilog;
using WebHook.WebHook.MinimalApi.Endpoints.Root;
using WebHook.WebHook.MinimalApi.Infracture;

var builder = WebApplication
    .CreateBuilder(args)
    .ConfigureApplicationBuilder();

var app = builder
    .Build()
    .ConfigureApplication()
    .MapAllEndpoints(); //Web route endpoint

try
{
    Log.Information("Starting host");
    app.Run();
    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}
