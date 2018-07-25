namespace OpenBrisk.Runtime.Core.Models
{
	// TODO: Custom context class to be able to remove the dependency on Microsoft.AspNetCore.Http.
	public delegate object MethodInvoker(object context);
}