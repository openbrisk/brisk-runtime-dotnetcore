namespace OpenBrisk.Runtime.Core.Interfaces
{
    public interface IFunction
    {
        IFunctionSettings FunctionSettings { get; }

        bool IsCompiled();
    }
}
