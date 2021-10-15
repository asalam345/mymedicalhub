using Manager.Service;
using Manager.Utility;
using DAL.Data;
using DAL.Repository;
using Domains;
using Interfaces.Repository;
using Interfaces.Service;
using Interfaces.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ViewModels;
using Manager.Helpers;
using Manager.Authorization;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace CRUDAspNetCore5WebAPI
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));
            services.AddCors();
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromDays(7);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
			//services.AddControllers();
			//services.AddHttpClient();


			//services.AddControllers().AddJsonOptions(x =>
			//{
			//	// serialize enums as strings in api responses (e.g. Role)
			//	x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
			//});
			// configure strongly typed settings object
			services.Configure<AppSettings>(_configuration.GetSection("AppSettings"));

            // configure DI for application services
            services.AddScoped<IJwtUtils, JwtUtils>();


            services.Configure<MailSettings>(_configuration.GetSection("MailSettings"));
            services.AddTransient<IMailService, MailService>();
            services.AddScoped<IRepository<User>, RepositoryUser>();
            services.AddTransient<IUser, RepositoryUser>();
            services.AddTransient<IRepository<Role>, RepositoryRole>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IRoleEnrollService, RoleEnrollService>();
            services.AddTransient<IRepository<RoleEnrollment>, RepositoryUserRoleEnroll>();

   //         services.AddCors(options =>
			//{
			//	options.AddDefaultPolicy(
			//		builder =>
			//		{
			//			builder.WithOrigins("*");
			//		});
			//});
			services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyMedicalhubAPI", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });

            //services.AddCors(c =>
            //{
            //	c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            //});
            services.AddSession();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => _configuration.Bind("JwtSettings", options))
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options => _configuration.Bind("CookieSettings", options));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddConsole(_configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();
            if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("https://localhost:44362/swagger/v1/swagger.json", "MyMedicalhubAPI v1"));
			}
			//else
			//{
			//	app.UseExceptionHandler("/Home/Error");
			//}
			//app.UseSession();
			//app.Use(async (context, next) =>
			//{
			//    var token = context.Session.GetString("Token");
			//    if (!string.IsNullOrEmpty(token))
			//    {
			//        context.Request.Headers.Add("Authorization", "Bearer " + token);
			//    }
			//    await next();
			//});
			app.UseCors(x => x
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
            app.UseHttpsRedirection();

            // global error handler
            app.UseMiddleware<ErrorHandlerMiddleware>();
            
            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

			
			app.UseSession(); // use this before .UseEndpoints
			app.UseEndpoints(endpoints =>
            {
	            endpoints.MapControllers();
            });
        }
    }
}
