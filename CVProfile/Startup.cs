using CoreLayer.AutoMapper;
using CoreLayer.IServices;
using CoreLayer.Services;
using CoreLayer.Services.FileManager;
using DataLayer.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVProfile
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
			services.AddControllersWithViews();

			services.AddAutoMapper(typeof(UserMappers));

			services.AddDbContext<CVContext>(option => option.UseSqlServer(Configuration.GetConnectionString("Default")));
			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			services.AddScoped<IFileManager, FileManager>();
			services.AddScoped<ISMS, SMS>();
			services.AddHttpContextAccessor();
			services.AddScoped<IRecaptcha, Recaptcha>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IOwnerService, OwnerService>();
			services.AddScoped<IOrderService, OrderService>();
			services.AddScoped<IMessageService, MessageService>();

			services.AddAuthentication(option =>
			{
				option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
			}).AddCookie(option =>
			{
				option.LoginPath = "/Register/Login";
				option.LogoutPath = "/Register/Logout";
				option.AccessDeniedPath = "/Register/AccessDenied";
				option.ExpireTimeSpan = TimeSpan.FromDays(30);
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
				endpoints.MapControllerRoute(
					name: "Admin",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}