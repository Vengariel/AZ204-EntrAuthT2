using Microsoft.Identity.Client;
using System.Text;
using static System.Net.WebRequestMethods;

namespace AZ204_EntrAuth.Clients
{
    public class ConfidentialClient : BaseAppClient, IConfidentialClient
    {
        static string secret;
        public async Task Build_Process(AppSettings appSettings, string[] scopes)
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
            //var newScopes = new string[] { "https://graph.microsoft.com/.default" }; // single  "https://outlook.office365.com/.default" 

            var res = await confidentialApp.AcquireTokenForClient(scopes).ExecuteAsync();
            sb.AppendLine("--------------------ACQUIRE TOKEN FOR CLIENT FLOW--------------------");
            await PrintTokenClaimsAndStalkAfterSuccess(sb, res);
            Console.ReadLine();
        }

        //static async Task ProcessOptions(IConfidentialClientApplication confidentialApp, string[] scopes)
        //{
        //    var sb = new StringBuilder();
        //    var newScopes = new string[] { "https://graph.microsoft.com/.default" }; // single  "https://outlook.office365.com/.default" 

        //    var res = await confidentialApp.AcquireTokenForClient(newScopes).ExecuteAsync();
        //    sb.AppendLine("--------------------ACQUIRE TOKEN FOR CLIENT FLOW--------------------");
        //    await PrintTokenClaimsAndStalkAfterSuccess(sb, res);
        //    Console.ReadLine();
        //    var og = new UserAssertion()
        //    confidentialApp.AcquireTokenOnBehalfOf
        //}


        //TODO IMPLEMENT OBN BEHALF OF FOR GRAPH FROM TOKEN

        //ar scopes = new[] { "https://graph.microsoft.com/.default" };

        //// Multi-tenant apps can use "common",
        //// single-tenant apps must use the tenant ID from the Azure portal
        //var tenantId = "common";

        //// Values from app registration
        //var clientId = "YOUR_CLIENT_ID";
        //var clientSecret = "YOUR_CLIENT_SECRET";

        //// using Azure.Identity;
        //var options = new OnBehalfOfCredentialOptions
        //{
        //    AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
        //};

        //// This is the incoming token to exchange using on-behalf-of flow
        //var oboToken = "JWT_TOKEN_TO_EXCHANGE";

        //var onBehalfOfCredential = new OnBehalfOfCredential(
        //    tenantId, clientId, clientSecret, oboToken, options);

        //var graphClient = new GraphServiceClient(onBehalfOfCredential, scopes);
    }
}
