using System;
using Microsoft.AspNetCore.Mvc;

namespace OpenBrisk.Runtime.Controllers
{
	[Route("/")]
	public class HealthController : Controller
	{
		[HttpGet("healthz")]
		public IActionResult Health() => this.Ok();
	}
}
