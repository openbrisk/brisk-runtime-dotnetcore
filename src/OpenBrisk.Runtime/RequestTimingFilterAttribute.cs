namespace OpenBrisk.Runtime
{
	using System.Diagnostics;
	using Microsoft.AspNetCore.Mvc.Filters;
	using Utils;

	public sealed class RequestTimingFilterAttribute : ActionFilterAttribute
	{
		private Stopwatch stopwatch;

		/// <inheritdoc />
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			this.stopwatch = new Stopwatch();

			base.OnActionExecuting(context);
		}

		/// <inheritdoc />
		public override void OnActionExecuted(ActionExecutedContext context)
		{
			this.stopwatch.Stop();

			context.HttpContext.Response.Headers.Add("X-OpenBrisk-Duration", new string[] {this.stopwatch.ElapsedNanoSeconds().ToString() });
			base.OnActionExecuted(context);
		}
	}
}