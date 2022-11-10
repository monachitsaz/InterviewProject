using InterviewProject.DataAccess;
using InterviewProject.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InterviewProject
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
            services.AddControllers();
            services.AddScoped(typeof(ISqlUtility), typeof(SqlUtility));
            services.AddScoped(typeof(IUserHelper), typeof(UserHelper));
            services.AddSwaggerGen(c =>
            {
                #region Register swagger
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "InterviewProject",
                    Description = "InterviewProject - Version01"
                });
                #endregion

                ////for documentation
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);

            });
            

            #region Cors
            services.AddCors(options =>
            {
                options.AddPolicy("InterviewProject",
                    builder =>
                    {
                        //here you can give special domain or ip
                        builder.WithOrigins("*");
                        builder.WithHeaders("*");
                        builder.WithMethods("*");

                    });
            });
            services.AddCors(options =>
            {
                options.AddPolicy("interview-project",
                    builder =>
                    {
                        //here you can give special domain or ip
                        builder.WithOrigins("*");
                        builder.WithHeaders("*");
                        builder.WithMethods("*");

                    });
            });
            #endregion
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FoodSoftware v1");
            });
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("InterviewProject");
            app.UseCors("interview-project");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
