using AZ204_EntrAuth;
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


    static Dictionary<string, string> SupportedFlows = new Dictionary<string, string>() { { "1", "Interactive" }, { "2", "Device Code" } };
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

    private static void Main(string[] args)
    {
        Console.WriteLine("Reading settings");
        var settings = GetAppSettings().Get<AppSettings>();

        //sin tenant

        // otro user

        // sin redirect
        string[] scopes = { "user.read" };
        //var localRedirectUrl = "http://localhost";

        var publicApp = PublicClientApplicationBuilder
            .Create(settings.AzureSettings.ClientId)
            .WithTenantId("common")
            .WithTenantId(settings.AzureSettings.TenantId)
            //.WithRedirectUri(localRedirectUrl) //KISS
            .WithDefaultRedirectUri() //method will set the public client application's redirect URI property to the default recommended redirect URI for public client applications.
            .Build();
        Console.WriteLine("Hello, World!");

        ProcessOptions(publicApp, scopes).GetAwaiter().GetResult();
        Environment.Exit(0);
    }

    static async Task ProcessOptions(IPublicClientApplication publicApp, string[] scopes)
    {
        var choosenOption = GetMenuUserOption();
        var sb = new StringBuilder();

        try
        {

            while (choosenOption != 0)
            {
                sb.Clear();
                switch (choosenOption)
                {
                    case 1://INTERACTIVE

                        var res = await publicApp.AcquireTokenInteractive(scopes).ExecuteAsync();
                        sb.AppendLine("--------------------INTERACTIVE FLOW--------------------");
                        await PrintTokenClaimsAndStalkAfterSuccess(sb, res);
                        choosenOption = GetMenuUserOption();
                        break;

                    case 2: //DEVICE
                        sb.AppendLine("--------------------DEVICE CODE FLOW--------------------");
                        var resDevice = await publicApp.AcquireTokenWithDeviceCode(scopes, deviceCode =>
                        {
                            Console.WriteLine(deviceCode.Message);
                            Console.WriteLine("Please hit Enter when you finish with browser login...in here's usually a polling put in place (netflix, etc) in order to react automatically after a few secs...");
                            Console.ReadLine();
                            return Task.CompletedTask;
                        }).ExecuteAsync(default);
                        await PrintTokenClaimsAndStalkAfterSuccess(sb, resDevice);
                        choosenOption = GetMenuUserOption();
                        break;

                }
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Console.ReadLine();
            Console.ReadLine();
        }
    }

    static byte GetMenuUserOption()
    {
        Console.WriteLine($"AZ204-2024 Study Group: Identity Planform Demo");
        Console.WriteLine($"Pick an option:");
        Console.WriteLine($"1 - Interactive token acq");
        Console.WriteLine($"2 - Device Code acq");
        Console.WriteLine($"{Environment.NewLine}");
        Console.WriteLine($"0 - TO EXIT");
        byte valuePicked = 0;

        var choice = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(choice) || !SupportedFlows.Keys.ToArray().Contains(choice) || !byte.TryParse(choice, out valuePicked))
        {
            Console.WriteLine("#FAIL, BYE");

        }
        return valuePicked;
    }


    //YES, I KNOW, doing mre than printing... lazy programming
    static async Task PrintTokenClaimsAndStalkAfterSuccess(StringBuilder sb, AuthenticationResult? authenticationResult)
    {
        sb.AppendLine($"Access Token: {authenticationResult?.AccessToken}");
        Append2EmptyLines(sb);
        sb.AppendLine($"Id Token: {authenticationResult?.IdToken}");


        foreach (var claim in authenticationResult?.ClaimsPrincipal.Identities.FirstOrDefault()?.Claims)
        {
            sb.AppendLine($"{claim.Type}: {claim.Value}");
        }

        Append2EmptyLines(sb);
        sb.AppendLine($"STALKING via Graph");
        sb.AppendLine($"{await FlurlIt.StalkThroughGraph(authenticationResult?.AccessToken)}");
        Console.WriteLine(sb.ToString());
    }

    #region private utility stuff
    private static void Append2EmptyLines(StringBuilder sb)
    {
        sb.AppendLine(Environment.NewLine);
        sb.AppendLine(Environment.NewLine);
    }
    #endregion private utility stuff


}