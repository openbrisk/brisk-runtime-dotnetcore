namespace OpenBrisk.Runtime.Core.Interfaces
{
    public interface IFunctionSettings
    {
        string ModuleName { get; }

        string FunctionHandler { get; }

        string AssemblyPath { get; }
        
        IFileContent<byte[]> Assembly { get; }
    }
}