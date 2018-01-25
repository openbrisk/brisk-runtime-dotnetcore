namespace OpenBrisk.Runtime.Core.Interfaces
{
    using System.Threading.Tasks;
	using OpenBrisk.Runtime.Shared;

	public interface IInvoker
    {
        Task<object> Execute(IFunction function, IBriskContext context);
    }
}
