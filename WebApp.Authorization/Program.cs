using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApp.Authorization.Data;
using WebApp.Authorization.Entities;

namespace WebApp.Authorization
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			builder.Services.AddControllersWithViews();
			builder.Services
				.AddDbContext<ApplicationDbContext>(config =>
				{
					config.UseNpgsql("Host=localhost;Port=5432;Database=webapp-db;Username=postgres;Password=1111;");
				})
				.AddIdentity<ApplicationUser, ApplicationRole>(config =>
				{
					config.Password.RequireDigit = false;
					config.Password.RequireNonAlphanumeric = false;
					config.Password.RequireLowercase = false;
					config.Password.RequireUppercase = false;
					config.Password.RequiredLength = 4;
				})
				.AddEntityFrameworkStores<ApplicationDbContext>();
			
			builder.Services.ConfigureApplicationCookie(config =>
			{
				config.LoginPath = "/admin/login";
				config.AccessDeniedPath = "/Admin/AccessDenied";
			});

			builder.Services.AddAuthorization(options =>
			{
				options.AddPolicy("Administrator", builder =>
				{
					builder.RequireRole("Administrator");
				});

				options.AddPolicy("Manager", builder =>
				{
					builder.RequireAssertion(context =>
					
						context.User.IsInRole("Manager") ||
						context.User.IsInRole("Administrator")
					);
				});
			});
			
			var app = builder.Build();

			using(var scope = app.Services.CreateScope())
			{
				DbInitializer.Init(scope.ServiceProvider);
			}
			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapDefaultControllerRoute();

			app.Run();
		}
		
	}
	public class DbInitializer()
	{
		public static void Init(IServiceProvider serviceProvider)
		{
			var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
			var user = new ApplicationUser
			{
				UserName = "bro",
				LastName = "Petrenko"
			};
			var result = userManager.CreateAsync(user, "1234").GetAwaiter().GetResult();
			if (result.Succeeded)
			{
				userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Administrator")).GetAwaiter().GetResult();
			}
		}
	}
}
