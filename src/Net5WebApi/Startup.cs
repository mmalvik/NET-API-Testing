using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Net5WebApi
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;

        public Startup(IWebHostEnvironment env)
        {
            _env = env;
        }

        public void Configure(IApplicationBuilder app)
        {
            if (_env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Net5WepApp v1"));
            }

            // Configure the HTTP request pipeline.
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(c => c.MapControllers());
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.
            services.AddControllers();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new() { Title = "Net5WepApp", Version = "v1" }); });
        }
    }
}