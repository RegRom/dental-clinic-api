using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DentalClinicBLL.MapperProfiles;
using DentalClinicBLL.Services;
using DentalClinicDAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;

namespace DentalClinicAPI
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
            services.AddDbContext<DentalClinicContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AzureConnection")));

            services.AddRouteServices();

            services.AddAutoMapper(typeof(AppointmentProfile).Assembly);
            services.AddAutoMapper(typeof(DentistProfile).Assembly);
            services.AddAutoMapper(typeof(PatientProfile).Assembly);
            services.AddAutoMapper(typeof(ProcedureProfile).Assembly);

            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new Info
            //    {
            //        Version = "v1",
            //        Title = "Dental Clinic API",
            //        Description = "Web API for an application developed for Dental Clinic",
            //        TermsOfService = "None",
            //        Contact = new Contact()
            //        {
            //            Name = "Romeo Rego",
            //            Email = "235005@student.pwr.edu.pl"
            //        }
            //    });
            //});

            services.AddCors(options =>
                options.AddDefaultPolicy(
                    builder =>
                    {
                        var url = Configuration["WebAppUrl"];

                        if (url.Equals("*"))
                        {
                            builder.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                        }
                        else
                        {
                            builder.WithOrigins(url)
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                        }
                    })
            );

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DentalClinicContext dentalClinicContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            //app.UseSwagger();
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dental Clinic API V1"); 
            //});

            app.UseCors();

            dentalClinicContext.Database.Migrate();
        }
    }
}
