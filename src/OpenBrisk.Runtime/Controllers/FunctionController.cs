﻿namespace OpenBrisk.Runtime.Controllers
{
	using System.IO;
	using System.Text;
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Mvc;
	using OpenBrisk.Runtime.Core.Interfaces;
	using OpenBrisk.Runtime.Shared;

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
            string data;
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {  
                data = await reader.ReadToEndAsync();
            }

            // TODO: Create custom context (dynamic?) from Request.
            object result = await this.invoker.Execute(this.function, new BriskContext(data));

            return this.GetSuitableActionResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            object result = await this.invoker.Execute(this.function, new BriskContext());
            return this.GetSuitableActionResult(result);
        }

        [HttpGet("/healthcheck")]
        public IActionResult Health() => this.Ok();    

        private IActionResult GetSuitableActionResult(object result)
        {
            if(result is string) 
            {
                return this.Ok(result);
            }

            if(result == null)
            {
                return this.Ok();
            }

            return this.Json(result);
        } 
    }
}
