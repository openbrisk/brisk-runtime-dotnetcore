namespace OpenBrisk.Runtime.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	
	[Route("/")]
	public class HealthController : Controller
	{
		[HttpGet("healthz")]
		public IActionResult Health() => this.Ok();
	}
}
