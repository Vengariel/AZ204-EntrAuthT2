using AZ204_EntrAuth;
using AZ204_EntrAuth.Clients;
using Microsoft.Extensions.Configuration;

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
    static Dictionary<string, string> SupportedFlows = new Dictionary<string, string>() {
        { "1", "Interactive" },
        { "2", "Device Code" },
         { "3", "Client Credentials" },
    };
    #endregion Settings

    public static async Task Main(string[] args)
    {
        Console.WriteLine("Reading settings");
        var settings = GetAppSettings().Get<AppSettings>() ?? new AppSettings();

        var choice = GetMenuUserOption();
        while (choice != 0)
        {
            BaseAppClient client;
            string[] scopes;
            switch (choice)
            {
                case 1:

                    client = new PublicClient();
                    scopes = ["user.read"];
                    await client.Build_Process(settings.AzureSettings.PublicClient, scopes, choice);
                    choice = GetMenuUserOption();
                    break;
                case 2:
                    client = new PublicClient();
                    scopes = ["user.read"];
                    await client.Build_Process(settings.AzureSettings.PublicClientDeviceCode, scopes, choice);
                    choice = GetMenuUserOption();
                    break;

                case 3:
                    client = new ConfidentialClient();
                    scopes = ["https://graph.microsoft.com/.default"];
                    await client.Build_Process(settings.AzureSettings.ConfidentialClient, scopes, default);
                    choice = GetMenuUserOption();
                    break;

            }

        }
        //sin tenant

        // otro user

        // sin redirect
    }

    static byte GetMenuUserOption()
    {
        Console.WriteLine($"AZ204-2024 Study Group: Identity Platform Demo");
        Console.WriteLine(Environment.NewLine);
        Console.WriteLine($"Pick Public Client option:");
        Console.WriteLine($"1 - Interactive token acq");
        Console.WriteLine($"2 - Device Code acq");
        Console.WriteLine(Environment.NewLine);

        Console.WriteLine($"Pick Confidential Client option:");
        Console.WriteLine($"3 - Token for Client acq");
        Console.WriteLine(Environment.NewLine);

        Console.WriteLine($"0 - TO EXIT");
        byte valuePicked = 0;

        var choice = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(choice) || !SupportedFlows.Keys.ToArray().Contains(choice) || !byte.TryParse(choice, out valuePicked))
        {
            Console.WriteLine("#FAIL, BYE");

        }
        return valuePicked;
    }
}