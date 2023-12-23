using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApp.Authorization.ViewModels;

namespace WebApp.Authorization.Controllers
{
	[Authorize]
	public class AdminController: Controller
	{
		public IActionResult Index()
		{
			return View();
		}
		[AllowAnonymous]
		public IActionResult Login(string ReturnUrl)
		{
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			var claims = new List<Claim>()
			{
				new Claim("demo", "value")
			};
			var identity = new ClaimsIdentity(claims, "Cookie");
			var claimPrincipal = new ClaimsPrincipal(identity);
			await HttpContext.SignInAsync("Cookie", claimPrincipal);
			
			return Redirect(model.ReturnUrl);
		}

		public async Task<IActionResult> LogOff()
		{
			
			await HttpContext.SignOutAsync("Cookie");
			return Redirect("/home/index");
		}
	}
}
