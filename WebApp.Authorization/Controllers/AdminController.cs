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
		public IActionResult AccessDenied(string ReturnUrl)
		{
			return View();
		}
		[Authorize(Policy = "Administrator")]
		public IActionResult Administrator()
		{
			return View();
		}
		[Authorize(Policy = "Manager")]
		public IActionResult Manager()
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
				new Claim(ClaimTypes.Name, model.UserName),
				new Claim(ClaimTypes.Role, "Administrator")
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
