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

            string codePathSetting = configuration["Compiler:CodePath"];
            Guard.AgainstEmpty(codePathSetting, "Compiler:CodePath");
            string codePath = Path.Combine(codePathSetting, string.Concat(moduleName, ".cs")); 
            StringContent code = new StringContent(codePath);

            string requirementsPathSetting = configuration["Compiler:RequirementsPath"];
            Guard.AgainstEmpty(requirementsPathSetting, "Compiler:RequirementsPath");
            string requirementsPath = Path.Combine(requirementsPathSetting, string.Concat(moduleName, ".csproj"));
            StringContent project = new StringContent(requirementsPath);

            string projectAssetsPath = Path.Combine(requirementsPathSetting, "obj", "project.assets.json");
            StringContent projectAssets = new StringContent(projectAssetsPath);

            string assemblyPathConfiguration = configuration["Compiler:FunctionAssemblyPath"];
            Guard.AgainstEmpty(assemblyPathConfiguration, "Compiler:FunctionAssemblyPath");
            string assemblyPath = Path.Combine(assemblyPathConfiguration, string.Concat(moduleName, ".dll"));
            BinaryContent assembly = new BinaryContent(assemblyPath);

            return new FunctionSettings(moduleName, functionHandler, code, project, projectAssets, assembly);
        }
    }
}