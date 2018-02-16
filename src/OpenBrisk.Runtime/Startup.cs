namespace OpenBrisk.Runtime
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using OpenBrisk.Runtime.Core.Interfaces;
	using OpenBrisk.Runtime.Core.Models;
    using OpenBrisk.Runtime.Utils;

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
			//string requirementsPath = this.Configuration["Compiler:RequirementsPath"];
			IFunction function = FunctionFactory.BuildFunction(this.Configuration);

			services.AddSingleton<IFunction>(function);
			services.AddSingleton<IFunctionSettings>(serviceProvider => serviceProvider.GetService<IFunction>().FunctionSettings);
			services.AddSingleton<IInvoker>(new DefaultInvoker());
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			// Add request timing middleware.
			app.Use(async (context, next) => 
			{
				Stopwatch stopwatch = new Stopwatch();

				context.Response.OnStarting(() => 
				{
					stopwatch.Stop();
					context.Response.Headers.Add("X-OpenBrisk-Duration", new string[] { stopwatch.ElapsedNanoSeconds().ToString() });
					return Task.CompletedTask;
				});

				stopwatch.Start();
				await next.Invoke();
			});

			app.UseMvc();
		}
	}
}
