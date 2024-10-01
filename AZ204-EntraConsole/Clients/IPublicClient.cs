
using Microsoft.Identity.Client;

namespace AZ204_EntrAuth.Clients
{
	public interface IPublicClient
	{
		Task<string> Build_Process(ClientSettings clientSettings, string[] scopes, byte choosenOption);
		Task<string> ProcessOptions(IPublicClientApplication publicApp, string[] scopes, byte choosenOption);
	}
}