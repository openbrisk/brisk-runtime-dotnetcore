namespace OpenBrisk.Runtime
{
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using OpenBrisk.Runtime.Core.Interfaces;
	using OpenBrisk.Runtime.Core.Models;

	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			this.Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();

			// Compile the function.
			string requirementsPath = this.Configuration["Compiler:RequirementsPath"];
			IFunction function = FunctionFactory.BuildFunction(this.Configuration);
			ICompiler compiler = new DefaultCompiler(new DefaultParser(), new DefaultReferencesManager(function.FunctionSettings));

			if (!function.IsCompiled())
			{
				compiler.Compile(function);
			}

			services.AddSingleton<IFunction>(function);
			services.AddSingleton<IFunctionSettings>(serviceProvider => serviceProvider.GetService<IFunction>().FunctionSettings);
			services.AddSingleton<IInvoker>(new DefaultInvoker(requirementsPath));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseMvc();
		}
	}
}
