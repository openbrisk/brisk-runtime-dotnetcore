
namespace OpenBrisk.Runtime.Controllers
{
    using System;
    using System.IO;
	using System.Reflection;
	using System.Text;
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Newtonsoft.Json;
	using OpenBrisk.Runtime.Core.Interfaces;
	using OpenBrisk.Runtime.Core.Models;

	[Route("/")]
	public class FunctionController : Controller
	{
		private readonly IFunction function;
		private readonly IInvoker invoker;

		public FunctionController(IFunction function, IInvoker invoker)
		{
			this.function = function;
			this.invoker = invoker;
		}

		[HttpPost]
		[RequestTimingFilter]
		public async Task<IActionResult> Post()
		{
			using (StreamReader streamReader = new StreamReader(this.Request.Body, Encoding.UTF8))
			using (JsonReader reader = new JsonTextReader(streamReader))
			{
				JsonSerializer serializer = new JsonSerializer();

				BriskContext context = new BriskContext
				{
					Data = serializer.Deserialize(reader),
				};

				// TODO: Check for 408 when timeout
				object result = await this.invoker.Execute(this.function, context);

				return this.GetSuitableActionResult(result);
			}
		}

		[HttpGet]
		[RequestTimingFilter]
		public async Task<IActionResult> Get()
		{
			BriskContext context = new BriskContext
			{
				Data = new { }
			};

			// TODO: Check for 408 when timeout
			object result = await this.invoker.Execute(this.function, context);
			return this.GetSuitableActionResult(result);
		}

		private IActionResult GetSuitableActionResult(dynamic result)
		{
			object responseResult = result;

<<<<<<< HEAD
			PropertyInfo property = result.GetType().GetProperty("forward");
			bool forwardResult = property != null;
=======
			// The result contains a 'result' target.
			if (result.GetType().GetProperty("result") != null)
			{
				responseResult = result.result;
			}
>>>>>>> b089c8c47aa5e3d75eabf01309b18a938aaa9cde

			// The result contains a 'forward' target.
			if (result.GetType().GetProperty("forward") != null)
			{
<<<<<<< HEAD
				responseResult = result.result;
				dynamic forward = result.forward;
				this.Response.Headers.Add("X-OpenBrisk-Forward", $"{forward.to}");
=======
				this.Response.Headers.Add("X-OpenBrisk-Forward", $"{result.forward}");
>>>>>>> b089c8c47aa5e3d75eabf01309b18a938aaa9cde
			}

			if (result is string)
			{
				return this.Ok(responseResult);
			}

			if (result == null)
			{
				return this.Ok();
			}

			return this.Json(responseResult);
		}
	}
}