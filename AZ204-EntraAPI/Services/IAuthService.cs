using AZ204_EntrAuth;
using AZ204_EntrAuth.Clients;

namespace AZ204_EntraAPI.Services
{
	public interface IAuthService
	{
		public Task<string> GetAccessToken();
	}

	public class AuthService : IAuthService
	{
		private readonly IPublicClient _publicClient;
		private readonly AppSettings _settings;

		public AuthService(IPublicClient publicClient, ISettingsProvider settingsProvider)
		{
			_publicClient = publicClient;
			_settings = settingsProvider.GetAppSettings().Get<AppSettings>() ?? new AppSettings();
		}

		public async Task<string> GetAccessToken()
		{
			var result = await _publicClient.Build_Process(_settings.AzureSettings.PublicClient, ["user.read"], 1);

			return result;
		}
	}
}
