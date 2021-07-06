using MembershipSystem.Api.Filters;
using MembershipSystem.Api.Services;
using MembershipSystem.Database;
using MembershipSystem.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.IO;

namespace MembershipSystem.Api
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

            //configures connection string to sql server getting DefaultConnection from appsettings
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //configure nuget package Session for login/logout functionality
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(5);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            //configures automapper nuget package to map properties to DTO models from models
            services.AddAutoMapper(typeof(Startup));

            //configures service to use throughout project
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ISessionService, SessionService>();
            services.AddHttpContextAccessor();


            //enables global expection filter to log errors not caught in try catches
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(MyExceptionFilter));
            });
            //configuring swagger nuget pakage for api documentation
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo 
                {   Version = "v1",
                    Title = "MembershipSystemAPI",
                    Description = "This is a Web API for operations of employees accounts"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "MembershipSystemAPI");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //configured in ConfigureServices method on line 22
            app.UseResponseCaching();

            //authentication and authorization middleware
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
    }
}
