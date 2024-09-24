﻿using Microsoft.Identity.Client;
using System.Text;

namespace AZ204_EntrAuth.Clients
{
    internal class PublicClient : BaseAppClient
    {
        static Dictionary<string, string> SupportedFlows = new Dictionary<string, string>() { { "1", "Interactive" }, { "2", "Device Code" } };

        static byte GetMenuUserOption()
        {
            Console.WriteLine($"AZ204-2024 Study Group: Identity Platform Demo");
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine($"Pick Public Client option:");
            Console.WriteLine($"1 - Interactive token acq");
            Console.WriteLine($"2 - Device Code acq");
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
        public static async Task Build_Process(AppSettings appSettings, string[] scopes)
        {
            var concreteCllientAppSettings = appSettings.AzureSettings.PublicClient;
            var publicApp = PublicClientApplicationBuilder
            .Create(concreteCllientAppSettings.ClientId)
            .WithTenantId("common")
            .WithTenantId(concreteCllientAppSettings.TenantId)
            //.WithRedirectUri(localRedirectUrl) //KISS
            .WithDefaultRedirectUri() //method will set the public client application's redirect URI property to the default recommended redirect URI for public client applications.
            .Build();
            Console.WriteLine("Hello, World!");

            await ProcessOptions(publicApp, scopes);
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
     

    }
}
