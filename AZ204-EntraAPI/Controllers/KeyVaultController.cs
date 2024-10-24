using AZ204_EntraAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AZ204_EntraAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class KeyVaultController : ControllerBase
	{
		[HttpGet("secret")]
		public async Task<ActionResult<string>> GetSecretAsync([FromServices] KeyVaultService keyVaultService, [FromQuery] string name)
		{
			var secret = await keyVaultService.GetSecretAsync(name);

			return Ok(secret);
		}
	}
}
