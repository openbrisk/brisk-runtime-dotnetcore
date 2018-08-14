namespace OpenBrisk.Runtime
{
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.Mvc;
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

	        // Build the function instance.
	        IFunction function = FunctionFactory.BuildFunction(this.Configuration);

	        services.AddSingleton<IFunction>(function);
	        services.AddSingleton<IFunctionSettings>(serviceProvider => serviceProvider.GetRequiredService<IFunction>().FunctionSettings);
	        services.AddSingleton<IInvoker, DefaultInvoker>();
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
