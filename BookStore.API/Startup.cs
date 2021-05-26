using BookStore.API.Contracts;
using BookStore.API.Data;
using BookStore.API.Mappings;
using BookStore.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace BookStore.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // Order of these actually matter
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", 
                    builder => 
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            services.AddAutoMapper(typeof(Maps));

            services.AddSwaggerGen(swaggerGenOptions => 
            {
                swaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo 
                { 
                    Title = "Book Store API", 
                    Version = "v1", 
                    Description = "This is an educational API for a book store."
                });

                // Path where project sits + name
                // Output for this project: BookStore.API.xml
                var xmlfile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

                var xmlpath = Path.Combine(AppContext.BaseDirectory, xmlfile);

                swaggerGenOptions.IncludeXmlComments(xmlpath);
            });

            services.AddSingleton<ILoggerService, LoggerService>();

            // Best to leave AddControllers for last
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(swaggerUIOptions => 
            {
                swaggerUIOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "Book Store API");
                swaggerUIOptions.RoutePrefix = "";
            });

            app.UseHttpsRedirection();

            app.UseCors("CorsPolicy");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
