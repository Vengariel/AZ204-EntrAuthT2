using Microsoft.Extensions.Configuration;

namespace AZ204_EntrAuth
{
	public class SettingsProvider : ISettingsProvider
	{
		public IConfigurationRoot GetAppSettings()
		{
			var envConfig = new ConfigurationBuilder().AddEnvironmentVariables().Build();
			var environment = envConfig?["Environment"] ?? envConfig?["ASPNETCORE_ENVIRONMENT"];

			var path = Directory.GetCurrentDirectory();
			return new ConfigurationBuilder()
				 .SetBasePath(path)
				 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				 .AddJsonFile($"appsettings.{environment}.json", optional: false, reloadOnChange: true)
				 .Build();
		}
	}
}