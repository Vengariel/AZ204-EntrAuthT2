using Microsoft.Identity.Client;
using System.Text;

namespace AZ204_EntrAuth.Clients
{
	public class ConfidentialClient : BaseAppClient, IConfidentialClient
	{
		public override async Task Build_Process(string[] scopes, byte chosenOption, string clientId, string tenantId, string clientSecret)
		{
			var confidentialClientIOptions = new ConfidentialClientApplicationOptions()
			{
				ClientId = clientId,
				ClientSecret = clientSecret,
				TenantId = tenantId
			};

			var confidentialApp = ConfidentialClientApplicationBuilder
				  .CreateWithApplicationOptions(confidentialClientIOptions)
				  .Build();

			Console.WriteLine("ConfidentialClient built");
			await ProcessOptions(confidentialApp, scopes);
		}

		public static async Task ProcessOptions(IConfidentialClientApplication confidentialApp, string[] scopes)
		{
			var sb = new StringBuilder();
			//var newScopes = new string[] { "https://graph.microsoft.com/.default" }; // single  "https://outlook.office365.com/.default" 

			var res = await confidentialApp.AcquireTokenForClient(scopes).ExecuteAsync();
			sb.AppendLine("--------------------ACQUIRE TOKEN FOR CLIENT FLOW--------------------");
			await PrintTokenClaimsAndStalkAfterSuccess(sb, res);
			Console.ReadLine();
		}
	}
}
