namespace OpenBrisk.Runtime.Core.Models
{
	using OpenBrisk.Runtime.Shared;

	// TODO: Custom context class to be able to remove the dependency on Microsoft.AspNetCore.Http.
	public delegate object MethodInvoker(IBriskContext context);
}