namespace OpenBrisk.Runtime.Core.Interfaces
{
    using System.Threading.Tasks;

	public interface IInvoker
    {
        Task<object> Execute(IFunction function, object context);
    }
}
