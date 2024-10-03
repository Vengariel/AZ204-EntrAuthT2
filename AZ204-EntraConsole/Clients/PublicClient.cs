using Microsoft.Identity.Client;
using System.Text;

namespace AZ204_EntrAuth.Clients
{
	public class PublicClient : BaseAppClient, IPublicClient
	{
		//static byte GetMenuUserOption()
		//{
		//    Console.WriteLine($"AZ204-2024 Study Group: Identity Platform Demo");
		//    Console.WriteLine(Environment.NewLine);
		//    Console.WriteLine($"Pick Public Client option:");
		//    Console.WriteLine($"1 - Interactive token acq");
		//    Console.WriteLine($"2 - Device Code acq");
		//    Console.WriteLine(Environment.NewLine);
		//    Console.WriteLine($"0 - TO EXIT");
		//    byte valuePicked = 0;

		//    var choice = Console.ReadLine();
		//    if (string.IsNullOrWhiteSpace(choice) || !SupportedFlows.Keys.ToArray().Contains(choice) || !byte.TryParse(choice, out valuePicked))
		//    {
		//        Console.WriteLine("#FAIL, BYE");

		//    }
		//    return valuePicked;
		//}

		public override async Task<string> Build_Process(ClientSettings clientSettings, string[] scopes, byte choosenOption)
		{
			var publicApp = PublicClientApplicationBuilder
			.Create(clientSettings.ClientId)
			//.WithTenantId("common")
			.WithTenantId(clientSettings.TenantId)
			.WithDefaultRedirectUri() //method will set the public client application's redirect URI property to the default recommended redirect URI for public client applications.
			.Build();
			Console.WriteLine("PublicClient built");

			return await ProcessOptions(publicApp, scopes, choosenOption);
		}

		public async Task<string> ProcessOptions(IPublicClientApplication publicApp, string[] scopes, byte choosenOption)
		{
			var accessToken = string.Empty;
			var sb = new StringBuilder();

			try
			{
				//while (choosenOption != 0)
				//{
				sb.Clear();
				switch (choosenOption)
				{
					case 1://INTERACTIVE

						var res = await publicApp.AcquireTokenInteractive(scopes).ExecuteAsync();
						sb.AppendLine("--------------------INTERACTIVE FLOW--------------------");
						await PrintTokenClaimsAndStalkAfterSuccess(sb, res);
						accessToken = res.AccessToken;
						break;

					case 2: //DEVICE
						sb.AppendLine("--------------------DEVICE CODE FLOW--------------------");
						var resDevice = await publicApp.AcquireTokenWithDeviceCode(scopes, deviceCode =>
						{
							Console.WriteLine(deviceCode.Message);
							Console.WriteLine("Please hit Enter when you finish with browser login...in here's usually a polling put in place (netflix, etc) in order to react automatically after a few secs...");
							// Console.ReadLine();
							return Task.CompletedTask;
						}).ExecuteAsync(default);
						await PrintTokenClaimsAndStalkAfterSuccess(sb, resDevice);
						accessToken = resDevice.AccessToken;
						break;
				}
				//}

			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
				Console.ReadLine();
				Console.ReadLine();
			}

			return accessToken;
		}
	}
}
