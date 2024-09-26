using Microsoft.Identity.Client;
using System.Text;

namespace AZ204_EntrAuth.Clients
{
    public class ConfidentialClient : BaseAppClient, IConfidentialClient
    {
        public async Task Build_Process(AppSettings appSettings, string[] scopes)
        {
            var concreteCllientAppSettings = appSettings.AzureSettings.ConfidentialClient;
            var confidentialClientIOptions = new ConfidentialClientApplicationOptions()
            {
                ClientId = concreteCllientAppSettings.ClientId,
                ClientSecret = concreteCllientAppSettings.Secret,
                TenantId = concreteCllientAppSettings.TenantId
            };

            var confidentialApp = ConfidentialClientApplicationBuilder
                 .CreateWithApplicationOptions(confidentialClientIOptions)
                 .Build();

            Console.WriteLine("ConfidentialClient built");
            await ProcessOptions(confidentialApp, scopes);
           // Environment.Exit(0);
        }

        public async Task ProcessOptions(IConfidentialClientApplication confidentialApp, string[] scopes)
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
