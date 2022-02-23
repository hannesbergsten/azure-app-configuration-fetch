using AppConfigurationFetcher.Extensions;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

configuration.ConnectToAppConfigurationStore(builder.Environment.IsDevelopment());

services.Configure<Settings>(builder.Configuration.GetSection("MyServiceSection"));
services.AddScoped(cfg => cfg.GetService<IOptionsSnapshot<Settings>>().Value);

var app = builder.Build();

app.UseAzureAppConfiguration();

app.MapGet("/", (Settings settings) =>
{
    return $"AppConfigurationStoreSecret: {settings.AppConfigurationStoreSecret}. AppSettingsProp: {settings.AppSettingsProp}";
}); 

app.Run();

public class Settings
{
    public string AppSettingsProp { get; set; }
    public string AppConfigurationStoreSecret { get; set; }
}