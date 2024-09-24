using AZ204_EntrAuth;
using AZ204_EntrAuth.Clients;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using System.Text;

internal class Program
{
    /// <summary>
    /// <see cref="https://learn.microsoft.com/en-us/entra/identity-platform/msal-client-application-configuration"/>
    /// </summary>
    //https://login.microsoftonline.com/<tenant>/ SINGLE ORG
    //https://login.microsoftonline.com/common/ WORK SCHOOL OR PERSONAL
    //https://login.microsoftonline.com/organizations/WORK SCHOOL
    //https://login.microsoftonline.com/consumers/ PERSONAL ONLY




    #region Settings
    static IConfigurationRoot GetAppSettings()
    {
        var envConfig = new ConfigurationBuilder().AddEnvironmentVariables().Build();
        var environment = envConfig?["Environment"];


        var path = Directory.GetCurrentDirectory();
        return new ConfigurationBuilder()
            .SetBasePath(path)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: false, reloadOnChange: true)
            .Build();

    }
    #endregion Settings

    private static async Task Main(string[] args)
    {
        Console.WriteLine("Reading settings");
        var settings = GetAppSettings().Get<AppSettings>();

        //sin tenant

        // otro user

        // sin redirect
        string[] scopes = { "user.read" };
        //var localRedirectUrl = "http://localhost";
        //  await PublicClient.Build_Process(settings, scopes);
       await ConfidentialClient.Build_Process(settings, scopes);

    }

}