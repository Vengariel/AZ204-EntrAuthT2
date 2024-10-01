using AZ204_EntrAuth.HttpClient;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AZ204_EntraAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class GraphController(IGraphHttpClient graphHttpClient) : Controller
	{
		private readonly IGraphHttpClient _graphHttpClient = graphHttpClient;

		[HttpGet]
		public async Task<IActionResult> Index([FromQuery][Required] string at)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var result = await _graphHttpClient.Get(at);

			return Ok(result);
		}
	}
}
