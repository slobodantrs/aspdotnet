using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SimpleWebServices
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

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
                endpoints.MapGet("/time", async context =>
                {
                    await context.Response.WriteAsync(DateTime.Now.ToString());
                });
                //http://localhost:5000/hello?name=Petar
                 endpoints.MapGet("/hello", async context =>
                {
                    var name=context.Request.Query["name"];
                    await context.Response.WriteAsync("Hello "+name);
                });
                endpoints.MapGet("/currency_conversion", async context =>
                {
                    var rsd=Convert.ToInt32(context.Request.Query["RSD"]);
                    var EUR=rsd/117.58;
                    var USD=rsd/104.09;
                    var CAD=rsd/81.82;
                    var response="{"+"\"EUR\":"+EUR+","+"\"USD\":"+USD+","+"\"CAD\":"+CAD+"}";
                    await context.Response.WriteAsync(response);
                });
            });
        }
    }
}
