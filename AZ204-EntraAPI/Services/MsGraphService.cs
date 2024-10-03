using AZ204_EntraAPI.Model;
using AZ204_EntrAuth;
using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Graph.Models;

namespace AZ204_EntraAPI.Services
{
	public class MsGraphService : IMsGraphService
	{
		//private readonly GraphServiceClient _graphServiceClient;
		private readonly ConfidentialClientSettings _clientSettings;

		public MsGraphService(ConfidentialClientSettings clientSettings)
		{
			_clientSettings = clientSettings;
		}

		/// <summary>
		/// Graph invitations only works for Azure AD, not Azure B2C
		/// </summary>
		public async Task<Invitation?> InviteUserAsync(UserModel userModel, string redirectUrl)
		{
			string[]? scopes = _clientSettings.Scopes;
			var tenantId = _clientSettings.TenantId;
			var clientId = _clientSettings.ClientId;
			var clientSecret = _clientSettings.Secret;
			var options = new TokenCredentialOptions
			{
				AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
			};

			var clientSecretCredential = new ClientSecretCredential(
				  tenantId, clientId, clientSecret, options);

			var graphServiceClient = new GraphServiceClient(
				  clientSecretCredential, scopes);

			var invitation = new Invitation
			{
				InvitedUserEmailAddress = userModel.Email,
				SendInvitationMessage = true,
				InviteRedirectUrl = redirectUrl,
				InvitedUserType = "guest" // default is guest,member
			};

			var invite = await graphServiceClient.Invitations.PostAsync(invitation);

			return invite;
		}

		// TODO: Implement the following methods
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

		// TODO: Implement the following methods
		//public async Task<User> GetGraphUser(string userId)
		//{
		//	return await _graphServiceClient.Users[userId]
		//		 .Request()
		//		 .GetAsync();
		//}
	}
}