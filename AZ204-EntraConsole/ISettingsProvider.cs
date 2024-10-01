using Microsoft.Extensions.Configuration;

namespace AZ204_EntrAuth
{
	public interface ISettingsProvider
	{
		IConfigurationRoot GetAppSettings();
	}
}