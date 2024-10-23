using AZ204_EntrAuth.Clients;

namespace AZ204_EntraAPI.Services
{
	public class AuthService(IPublicClient publicClient) : IAuthService
	{
		private readonly IPublicClient _publicClient = publicClient;

		public async Task<string> GetAccessToken(string tenantId, string clientId, string clientSecret)
		{
			string[] scopes = ["User.Read"];
			var result = await _publicClient.Build_Process(scopes, 1, clientId, tenantId, clientSecret);

			return result;
		}
	}
}
