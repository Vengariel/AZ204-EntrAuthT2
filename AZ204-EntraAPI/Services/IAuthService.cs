namespace AZ204_EntraAPI.Services
{
	public interface IAuthService
	{
		public Task<string> GetAccessToken(string tenantId, string clientId, string clientSecret);
	}
}
