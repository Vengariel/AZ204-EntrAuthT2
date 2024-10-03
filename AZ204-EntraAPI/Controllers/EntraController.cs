using AZ204_EntraAPI.Model;
using AZ204_EntraAPI.Services;
using AZ204_EntrAuth;
using Microsoft.AspNetCore.Mvc;

namespace AZ204_EntraAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EntraController(IAuthService authService, IMsGraphService msGraphService, AppSettings appSettings) : ControllerBase
	{
		private readonly IAuthService _authService = authService;
		private readonly IMsGraphService _msGraphService = msGraphService;
		private readonly AppSettings _appSettings = appSettings;

		[HttpPost(nameof(Auth))]
		public async Task<IActionResult> Auth()
		{
			var tokenResult = await _authService.GetAccessToken();

			return Ok(tokenResult);
		}

		[HttpPost(nameof(Join))]
		public async Task<IActionResult> Join()
		{
			var result = await _msGraphService.InviteUserAsync(
						userModel: new UserModel { Email = "jceron@apexsystems.com" },
						_appSettings.AzureSettings.RedirectUrl);

			return Ok(result);
		}
	}
}
