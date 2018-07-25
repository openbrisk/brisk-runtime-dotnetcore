namespace OpenBrisk.Runtime.Core.Models
{
    using OpenBrisk.Runtime.Core.Interfaces;
    
    public sealed class FunctionSettings : IFunctionSettings
    {
        public FunctionSettings(string moduleName, string functionHandler, string assemblyPath, IFileContent<byte[]> assembly)
        {
            this.ModuleName = moduleName;
            this.FunctionHandler = functionHandler;
            this.AssemblyPath = assemblyPath;
            this.Assembly = assembly;
        }

        public string ModuleName { get; private set; }

        public string FunctionHandler { get; private set; }

        public string AssemblyPath { get; private set; }
        
        public IFileContent<byte[]> Assembly { get; private set; }
    }
}
