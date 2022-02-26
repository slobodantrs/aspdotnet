using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Products
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseDefaultFiles();
            app.UseStaticFiles();
            
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/time", async context =>
                {
                    await context.Response.WriteAsync("Hello "+DateTime.Now.ToString());
                });
                endpoints.MapGet("/day", async context =>
                {
                    await context.Response.WriteAsync(DateTime.Now.DayOfWeek.ToString());
                });
                endpoints.MapGet("/hello", async context =>
                {
                    var name=context.Request.Query["name"];
                    await context.Response.WriteAsync("Hello "+name);
                });
            });
        }
    }
}
