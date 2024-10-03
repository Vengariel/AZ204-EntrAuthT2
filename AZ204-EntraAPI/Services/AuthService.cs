using AZ204_EntrAuth;
using AZ204_EntrAuth.Clients;

namespace AZ204_EntraAPI.Services
{
	public class AuthService(IPublicClient publicClient, ISettingsProvider settingsProvider) : IAuthService
	{
		private readonly IPublicClient _publicClient = publicClient;
		private readonly AppSettings _settings = settingsProvider.GetAppSettings().Get<AppSettings>() ?? new AppSettings();

		public async Task<string> GetAccessToken()
		{
			string[] scopes = ["User.Read"];
			var result = await _publicClient.Build_Process(_settings.AzureSettings.PublicClient, scopes, 1);

			return result;
		}
	}
}
