﻿namespace OpenBrisk.Runtime.Core.Models
{
	using OpenBrisk.Runtime.Core.Interfaces;
	using System;
	using System.Linq;
	using System.Reflection;
	using System.Threading.Tasks;
	using OpenBrisk.Runtime.Core.Extensions;
    using System.IO;

    public class DefaultInvoker : IInvoker
	{
		private InvocationData invocationData;

		public async Task<object> Execute(IFunction function, object context = null)
		{
			object result = this.ExecuteFunction(function, context);
			if (result is Task task)
			{
				return await task.TimeoutAfter(TimeSpan.FromSeconds(10)); // TODO: Read timout from environment.
			}
			else
			{
				return result;
			}
		}

		private object ExecuteFunction(IFunction function, object context)
		{
			// Create invocation data if none exists. This caches assembly and type loading.
			if (this.invocationData == null)
			{
				this.invocationData = this.CreateInvocationData(function);
			}

			// TODO: Custom context class to be able to remove the dependency on Microsoft.AspNetCore.Http.
			object returnedValue = this.invocationData.MethodInvoker.Invoke(context);
			return returnedValue;
		}

		private InvocationData CreateInvocationData(IFunction function)
		{
			// Load the function assembly.
			Assembly assembly = Assembly.Load(function.FunctionSettings.Assembly.Content);

			// Register handler for the assembly resolve event to load the required assemblies at runtime.
			AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
			{
				AssemblyName referenceAssembly = args.RequestingAssembly.GetReferencedAssemblies().FirstOrDefault(x => x.FullName == args.Name);	
				string assemblyFileName = string.Concat(referenceAssembly.Name.Split(',').First().Trim().TrimEnd('.'), ".dll");	
				string assemblyFilePath = Path.Combine(function.FunctionSettings.AssemblyPath, assemblyFileName);
				
				FileInfo assemblyFile = new FileInfo(assemblyFilePath);
				return Assembly.LoadFile(assemblyFile.FullName);
			};

			// Get the functions class and create an instance of it.
			Type type = assembly.GetExportedTypes().FirstOrDefault(x => x.Name == function.FunctionSettings.ModuleName);
			object instance = Activator.CreateInstance(type);

			// Create and cache delegate to speed up the reflection calls.
			MethodInfo method = type.GetMethod(function.FunctionSettings.FunctionHandler, BindingFlags.Instance | BindingFlags.Public);
			MethodInvoker methodInvoker = (MethodInvoker)method.CreateDelegate(typeof(MethodInvoker), instance);

			return new InvocationData
			{
				FunctionAssembly = assembly,
				FunctionType = type,
				FunctionInstance = instance,
				MethodInvoker = methodInvoker,
			};
		}
	}
}
