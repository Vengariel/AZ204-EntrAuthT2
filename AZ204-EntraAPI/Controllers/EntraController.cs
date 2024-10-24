using AZ204_EntraAPI.Model;
using AZ204_EntraAPI.Services;
using AZ204_EntrAuth;
using Microsoft.AspNetCore.Mvc;

namespace AZ204_EntraAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EntraController(IAuthService authService, IMsGraphService msGraphService, AppSettings appSettings, KeyVaultService keyVaultService) : ControllerBase
	{
		private readonly IAuthService _authService = authService;
		private readonly IMsGraphService _msGraphService = msGraphService;
		private readonly AppSettings _appSettings = appSettings;
		private readonly KeyVaultService _keyVaultService = keyVaultService;

		[HttpPost(nameof(Auth))]
		public async Task<IActionResult> Auth()
		{
			// TODO: Get these from key vault
			string clientId = await _keyVaultService.GetSecretAsync(secretName: "confidentialClientId");
			string tenantId = await _keyVaultService.GetSecretAsync(secretName: "confidentialClientTenantId");
			var tokenResult = await _authService.GetAccessToken(tenantId, clientId, string.Empty);

			return Ok(tokenResult);
		}

		[HttpPost(nameof(Join))]
		public async Task<IActionResult> Join([FromBody] string email = "jceron@apexsystems.com")
		{
			var userModel = new UserModel { Email = email };
			var result = await _msGraphService.InviteUserAsync(userModel);

			return Ok(result);
		}
	}
}
