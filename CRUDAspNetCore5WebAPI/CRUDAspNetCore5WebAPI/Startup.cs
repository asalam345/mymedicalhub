using CRUD_BAL.Service;
using CRUD_BAL.Utility;
using CRUD_DAL.Data;
using CRUD_DAL.Repository;
using Domains;
using Interfaces.Repository;
using Interfaces.Service;
using Interfaces.Utility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels;

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
            //services.AddControllers();
            services.AddHttpClient();
            services.Configure<MailSettings>(_configuration.GetSection("MailSettings"));
            services.AddTransient<IMailService, MailService>();
            services.AddScoped<IRepository<User>, RepositoryUser>();
            services.AddTransient<IUser, RepositoryUser>();
            services.AddTransient<IRepository<Role>, RepositoryRole>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IRoleEnrollService, RoleEnrollService>();
            services.AddTransient<IRepository<RoleEnrollment>, RepositoryUserRoleEnroll>();
            services.AddCors(options =>
			{
				options.AddDefaultPolicy(
					builder =>
					{
						builder.WithOrigins("*");
					});
			});
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

		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("https://localhost:44362/swagger/v1/swagger.json", "MyMedicalhubAPI v1"));
            }
            app.UseCors(x => x
          .AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseDeveloperExceptionPage();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
			//app.UseEndpoints(endpoints =>
   //         {
   //             endpoints.MapControllerRoute(name: "role",
   //                         pattern: "role",
   //                         defaults: new { controller = "Role", action = "Index" });
   //             endpoints.MapControllerRoute(name: "default",
   //                         pattern: "{controller=UserDetails}/{action=Index}/{id?}");
   //         });
        }
    }
}
