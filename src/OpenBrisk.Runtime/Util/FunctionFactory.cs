namespace OpenBrisk.Runtime
{
	using System;
	using System.IO;
	using Microsoft.Extensions.Configuration;
	using OpenBrisk.Runtime.Core.Interfaces;
	using OpenBrisk.Runtime.Core.Models;
	using OpenBrisk.Runtime.Utils;

	public static class FunctionFactory
    {
        public static IFunction BuildFunction(IConfiguration configuration)
        {
            IFunctionSettings settings = BuildFunctionSettings(configuration);
            return new Function(settings);
        }

        private static IFunctionSettings BuildFunctionSettings(IConfiguration configuration)
        {
            string moduleName = Environment.GetEnvironmentVariable("MODULE_NAME");
            Guard.AgainstEmpty(moduleName, "MODULE_NAME");

            string functionHandler = Environment.GetEnvironmentVariable("FUNCTION_HANDLER");
            Guard.AgainstEmpty(functionHandler, "FUNCTION_HANDLER");

            string functionPath = configuration["OpenBrisk:FunctionPath"];
            Guard.AgainstEmpty(functionPath, "OpenBrisk:FunctionPath");

            string assemblyPath = Path.Combine(functionPath, "out");       
            string assemblyFilePath = Path.Combine(assemblyPath, string.Concat(moduleName, ".dll"));
            BinaryContent assembly = new BinaryContent(assemblyFilePath);

            return new FunctionSettings(moduleName, functionHandler, assemblyPath, assembly);
        }
    }
}