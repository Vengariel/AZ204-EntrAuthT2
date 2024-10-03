using AZ204_EntrAuth;
using AZ204_EntrAuth.HttpClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.Models;
using System.ComponentModel.DataAnnotations;

namespace AZ204_EntraAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class GraphController : Controller
	{
		private readonly IGraphHttpClient _graphHttpClient;
		private readonly MsGraphService _msGraphService;

		public GraphController(
			IGraphHttpClient graphHttpClient
			)
		{
			_graphHttpClient = graphHttpClient;
			var settings = new SettingsProvider().GetAppSettings().Get<AppSettings>() ?? new AppSettings();
			_msGraphService = new MsGraphService(settings.AzureSettings.ConfidentialClient);
		}

		[HttpGet]
		public async Task<IActionResult> Index([FromQuery][Required] string at, [FromQuery] string? restOfUrl = "me")
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var result = await _graphHttpClient.Get(at, restOfUrl);

			return Ok(result);
		}

		// TODO: The API consumer should be able to query the Graph API to get the application
		// information. "https://graph.microsoft.com/v1.0/applications"

		// TODO: The API consumer should be able to request to join an organization.
		// and redirect to "https://myapplications.microsoft.com/?tenantid={our tenant ID}"

		// POST endpoint to request join to a Entra organization
		[HttpPost("join")]
		public async Task<IActionResult> JoinOrganization(
			//[FromBody][Required] UserModel userModel
			)
		{
			//if (!ModelState.IsValid)
			//{
			//	return BadRequest(ModelState);
			//}

			await CreateUserAsync(new UserModel { Email = "jceron@apexsystems.com" });

			return Ok(true);
		}

		private async Task<(Invitation? UserModel, string Error)> CreateUserAsync(UserModel userModel)
		{
			var result = await _msGraphService.InviteUser(
				userModel,
				"https://myapplications.microsoft.com/?tenantid=98db5eea-3600-4ffc-8b5f-c4987cdcfe67");

			return (result, string.Empty);
		}
	}
}