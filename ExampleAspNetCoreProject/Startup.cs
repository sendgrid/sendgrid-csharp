using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SendGrid;

namespace ExampleAspNetCoreProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configure the SendGrid client based on the values specified in the application settings.
            // We then register the Client and its options with the DI container, passing a singleton HttpClient instance
            // to be used for making requests to the API. This allows us to inject the client into our controllers.
            services.Configure<SendGridClientOptions>(Configuration.GetSection("SendGrid"));

            services.AddSingleton(new HttpClient());
            services.AddSingleton<ISendGridClient, SendGridClient>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
