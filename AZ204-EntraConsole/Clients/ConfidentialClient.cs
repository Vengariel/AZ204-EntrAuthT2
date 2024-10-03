using Microsoft.Identity.Client;
using System.Text;

namespace AZ204_EntrAuth.Clients
{
	public class ConfidentialClient : BaseAppClient, IConfidentialClient
	{
		public override async Task Build_Process(ClientSettings clientSettings, string[] scopes, byte chosenOption)
		{
			var confidentialClientIOptions = new ConfidentialClientApplicationOptions()
			{
				ClientId = clientSettings.ClientId,
				ClientSecret = ((ConfidentialClientSettings)clientSettings).Secret,
				TenantId = clientSettings.TenantId
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
