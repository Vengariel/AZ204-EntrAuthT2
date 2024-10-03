// web api controller to check API heatlh
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace AZ204_EntrAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HealthController : ControllerBase
	{
		[HttpGet]
		public IActionResult Get()
		{
			var sb = new StringBuilder();
			sb.AppendLine("API is healthy");
			sb.AppendLine($"Time: {DateTime.Now}");
			return Ok(sb.ToString());
		}
	}
}