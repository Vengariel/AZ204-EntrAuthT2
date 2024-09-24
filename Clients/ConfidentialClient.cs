using Microsoft.Identity.Client;
using System.Text;
using static System.Net.WebRequestMethods;

namespace AZ204_EntrAuth.Clients
{
    internal class ConfidentialClient : BaseAppClient
    {
        static string secret;
        public static async Task Build_Process(AppSettings appSettings, string[] scopes)
        {
            var concreteCllientAppSettings = appSettings.AzureSettings.ConfidentialClient;
            var confidentialClientIOptions = new ConfidentialClientApplicationOptions()
            {
                ClientId = concreteCllientAppSettings.ClientId,
                ClientSecret = concreteCllientAppSettings.Secret,
                TenantId = concreteCllientAppSettings.TenantId
            };

            secret = concreteCllientAppSettings.Secret;//todo remove antipattern
            var confidentialApp = ConfidentialClientApplicationBuilder
                .CreateWithApplicationOptions(confidentialClientIOptions)
                .Build();
            //    .Create(appSettings.AzureSettings.ClientId)
            //.Create(appSettings.AzureSettings.ClientId)
            //.WithTenantId("common")
            //.WithTenantId(appSettings.AzureSettings.TenantId)
            ////.WithRedirectUri(localRedirectUrl) //KISS
            //.WithDefaultRedirectUri() //method will set the public client application's redirect URI property to the default recommended redirect URI for public client applications.
            //.Build();
            //Console.WriteLine("Hello, World!");
            Console.WriteLine("Hello, World!");
            await ProcessOptions(confidentialApp, scopes);
            Environment.Exit(0);
        }

        static async Task ProcessOptions(IConfidentialClientApplication confidentialApp, string[] scopes)
        {
            var sb = new StringBuilder();
            var newScopes = new string[] { "https://graph.microsoft.com/.default" }; // single  "https://outlook.office365.com/.default" 

            var res = await confidentialApp.AcquireTokenForClient(newScopes).ExecuteAsync();
            sb.AppendLine("--------------------ACQUIRE TOKEN FOR CLIENT FLOW--------------------");
            await PrintTokenClaimsAndStalkAfterSuccess(sb, res);
            Console.ReadLine();
        }
    }
}
