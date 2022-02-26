using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace SimpleWebApiWithDatabase
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            string connString=Configuration["ConnectionString:ProductCatalog"];
            var connection=new SqlConnection(connString);
            services.AddTransient<IDbConnection>(_=>new SqlConnection(connString));
             services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowOrigin",
                builder =>
                    {
                            builder.WithOrigins("https://localhost:5021", "http://localhost:4200")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                            });
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
           //     app.UseSwagger();
          //      app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SimpleWebApiWithDatabase v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
             app.UseCors("AllowOrigin");
           

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
