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
				});
			builder.Services.AddAuthorization();
			
			var app = builder.Build();
			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapDefaultControllerRoute();

			app.Run();
		}
	}
}
