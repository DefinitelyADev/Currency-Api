using CurrencyApi.Infrastructure.Core;
using CurrencyApi.Infrastructure.Core.Engine;
using CurrencyApi.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace CurrencyApi.Presentation
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) => services.ConfigureApplicationServices(_configuration, _webHostEnvironment);
        // services.AddControllers();
        // services.AddSwaggerGen(c =>
        // {
        //     c.SwaggerDoc("v1", new OpenApiInfo {Title = "Currency Api", Version = "v1"});
        // });



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ConfigureRequestPipeline();
            app.StartEngine();

            // app.UseHttpsRedirection();
            //
            // app.UseRouting();
            //
            // app.UseAuthorization();
            //
            // app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
