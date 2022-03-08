using CoreLayer.AutoMapper;
using CoreLayer.Hubs;
using CoreLayer.IServices;
using CoreLayer.Services;
using DataLayer.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace CVProfile
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
			services.AddControllersWithViews();

			services.AddAutoMapper(typeof(UserMappers));

			services.AddDbContext<CVContext>(option => option.UseSqlServer(Configuration.GetConnectionString("Default")));
			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			services.AddScoped<IFileManager, FileManager>();
			services.AddScoped<ISMS, NiazpardazSMS>();
			services.AddHttpContextAccessor();
			services.AddScoped<IRecaptcha, Recaptcha>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IOwnerService, OwnerService>();
			services.AddScoped<IOrderService, OrderService>();
			services.AddScoped<IMessageService, MessageService>();
			services.AddTransient<Isapl, sapl>();

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
			services.AddSignalR();
			services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromMinutes(5);
			});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}
			app.UseSession();
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
				endpoints.MapHub<UploadHub>("/uploadhub");
			});
		}
	}
}
