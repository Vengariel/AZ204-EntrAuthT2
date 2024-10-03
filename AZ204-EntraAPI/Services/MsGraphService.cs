using AZ204_EntraAPI.Model;
using AZ204_EntrAuth;
using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Graph.Models;

namespace AZ204_EntraAPI.Services
{
	public class MsGraphService : IMsGraphService
	{
		private readonly GraphServiceClient _graphServiceClient;

		public MsGraphService(ConfidentialClientSettings clientSettings)
		{
			string[]? scopes = clientSettings.Scopes;

			var tenantId = clientSettings.TenantId;

			// Values from app registration
			var clientId = clientSettings.ClientId;

			var clientSecret = clientSettings.Secret;

			var options = new TokenCredentialOptions
			{
				AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
			};

			// https://docs.microsoft.com/dotnet/api/azure.identity.clientsecretcredential
			var clientSecretCredential = new ClientSecretCredential(
				  tenantId, clientId, clientSecret, options);

			_graphServiceClient = new GraphServiceClient(
				  clientSecretCredential, scopes);
		}

		/// <summary>
		/// Graph invitations only works for Azure AD, not Azure B2C
		/// </summary>
		public async Task<Invitation?> InviteUserAsync(UserModel userModel, string redirectUrl)
		{
			var invitation = new Invitation
			{
				InvitedUserEmailAddress = userModel.Email,
				SendInvitationMessage = true,
				InviteRedirectUrl = redirectUrl,
				InvitedUserType = "guest" // default is guest,member
			};

			var invite = await _graphServiceClient.Invitations.PostAsync(invitation);

			return invite;
		}

		//public async Task<User?> UserExistsAsync(string email)
		//{
		//	var users = await _graphServiceClient.Users
		//		 .Request()
		//		 .Filter($"mail eq '{email}'")
		//		 .GetAsync();

		//	if (users.CurrentPage.Count == 0)
		//		return null;

		//	return users.CurrentPage[0];
		//}

		//public async Task<User> GetGraphUser(string userId)
		//{
		//	return await _graphServiceClient.Users[userId]
		//		 .Request()
		//		 .GetAsync();
		//}
	}
}