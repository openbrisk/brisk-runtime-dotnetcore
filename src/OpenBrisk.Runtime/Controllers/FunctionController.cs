
namespace OpenBrisk.Runtime.Controllers
{
	using System.IO;
	using System.Reflection;
	using System.Text;
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Mvc;
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
		public async Task<IActionResult> Post()
		{
			// string data;
			// using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
			// {  
			//     data = await reader.ReadToEndAsync();
			// }

			using (StreamReader streamReader = new StreamReader(this.Request.Body, Encoding.UTF8))
			using (JsonReader reader = new JsonTextReader(streamReader))
			{
				JsonSerializer serializer = new JsonSerializer();

				BriskContext context = new BriskContext
				{
					Data = serializer.Deserialize(reader),
				};

				object result = await this.invoker.Execute(this.function, context);

				return this.GetSuitableActionResult(result);
			}
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			BriskContext context = new BriskContext
			{
				Data = new { }
			};

			object result = await this.invoker.Execute(this.function, context);
			return this.GetSuitableActionResult(result);
		}

		private IActionResult GetSuitableActionResult(dynamic result)
		{
			object responseResult = result;

			PropertyInfo property = result.GetType().GetProperty("forward");
			bool forwardResult = property != null;

			if(forwardResult) 
			{
				responseResult = result.result;
				dynamic forward = result.forward;
				this.Response.Headers.Add("X-OpenBrisk-Forward", $"{forward.to}");
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