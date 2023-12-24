namespace WebApp.Authorization
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			builder.Services.AddControllersWithViews();

			builder.Services
				.AddAuthentication("Cookie")
				.AddCookie("Cookie", config =>
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
			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapDefaultControllerRoute();

			app.Run();
		}
	}
}
