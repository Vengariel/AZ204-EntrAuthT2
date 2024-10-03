using AZ204_EntrAuth.HttpClient;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AZ204_EntraAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class GraphController(
	  IGraphHttpClient graphHttpClient
		  ) : Controller
	{
		private readonly IGraphHttpClient _graphHttpClient = graphHttpClient;

		// TODO: 2.- The API consumer should be able to query the Graph API to get the user information.
		// "https://graph.microsoft.com/v1.0/me"
		[HttpGet(nameof(Me))]
		public async Task<IActionResult> Me([FromQuery][Required] string accessToken)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			return Ok(true);
		}

		// TODO: 3.- The API consumer should be able to query the Graph API to get the application
		// information. "https://graph.microsoft.com/v1.0/applications"
		[HttpGet(nameof(Applications))]
		public async Task<IActionResult> Applications([FromQuery][Required] string accessToken)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			return Ok(true);
		}
	}
}