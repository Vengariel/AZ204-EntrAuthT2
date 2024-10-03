using AZ204_EntraAPI.Model;
using Microsoft.Graph.Models;

namespace AZ204_EntraAPI.Services
{
	public interface IMsGraphService
	{
		Task<Invitation?> InviteUserAsync(UserModel userModel, string redirectUrl);
	}
}