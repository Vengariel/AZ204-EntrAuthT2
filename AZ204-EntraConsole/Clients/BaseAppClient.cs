using AZ204_EntrAuth.HttpClient;
using Microsoft.Identity.Client;
using System.Security.Claims;
using System.Text;

namespace AZ204_EntrAuth.Clients
{
	public abstract class BaseAppClient
	{
		protected static async Task PrintTokenClaimsAndStalkAfterSuccess(StringBuilder sb, AuthenticationResult? authenticationResult)
		{
			sb.AppendLine($"Access Token: {authenticationResult?.AccessToken}");
			Append2EmptyLines(sb);
			sb.AppendLine($"Id Token: {authenticationResult?.IdToken}");
			Append2EmptyLines(sb);
			foreach (var claim in authenticationResult?.ClaimsPrincipal?.Identities?.FirstOrDefault()?.Claims ?? [])
			{
				sb.AppendLine($"{claim.Type}: {claim.Value}");
			}

			Append2EmptyLines(sb);
			sb.AppendLine($"STALKING via Graph");

			var graphHttpClient = new GraphHttpClient();

			sb.AppendLine($"{await graphHttpClient.Get(authenticationResult?.AccessToken ?? string.Empty)}");
			Console.WriteLine(sb.ToString());
		}
		public abstract Task Build_Process(ClientSettings clientSettings, string[] scopes, byte chosenOption);

		#region private utility stuff
		private static void Append2EmptyLines(StringBuilder sb)
		{
			sb.AppendLine(Environment.NewLine);
			sb.AppendLine(Environment.NewLine);
		}
		#endregion private utility stuff
	}
}
