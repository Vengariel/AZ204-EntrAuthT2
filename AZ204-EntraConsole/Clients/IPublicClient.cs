
using Microsoft.Identity.Client;

namespace AZ204_EntrAuth.Clients
{
	public interface IPublicClient
	{
		Task<string> Build_Process(string[] scopes, byte choosenOption, string clientId, string tenantId, string clientSecret);
		Task<string> ProcessOptions(IPublicClientApplication publicApp, string[] scopes, byte choosenOption);
	}
}