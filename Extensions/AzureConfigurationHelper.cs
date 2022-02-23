using Azure.Identity;

namespace AppConfigurationFetcher.Extensions;
public static class AzureConfigurationHelper
{
    public static void ConnectToAppConfigurationStore(this ConfigurationManager configuration, bool isDevelopment = false, string appConfigurationUrl = "https://my-configuration-store.azconfig.io")
    {
        configuration.AddAzureAppConfiguration(options =>
        {
            var credentials = new DefaultAzureCredential();
            if (isDevelopment)
            {
                credentials = new DefaultAzureCredential(
                    new DefaultAzureCredentialOptions
                    {
                        VisualStudioTenantId = configuration.GetValue<string>("AADTenantId")
                    });
            }

            var labelFilter = "possibleLabelFilter";

            options.Connect(new Uri(appConfigurationUrl), credentials)
                .Select(keyFilter: "possibleKeyFilter", labelFilter: labelFilter);

            options.ConfigureRefresh(refresh =>
            {
                refresh.Register($"RefreshAll", labelFilter, true);
                refresh.SetCacheExpiration(TimeSpan.FromSeconds(5));
            });
        });
    }
}