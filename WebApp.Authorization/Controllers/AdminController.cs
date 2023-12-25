using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApp.Authorization.Data;
using WebApp.Authorization.ViewModels;
using WebApp.Authorization.Entities;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Authorization.Controllers
{
	[Authorize]
	public class AdminController: Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AdminController(
			UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager)
        {
			this._userManager = userManager;
			this._signInManager = signInManager;
		}
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

			var user = await _userManager.FindByNameAsync(model.UserName);
			
			if (user == null)
			{
				ModelState.AddModelError("Username", "User not found");
				return View(model);
			}

			var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
			if (result.Succeeded)
			{
				return Redirect(model.ReturnUrl);

			}
			return View(model);
			
		}

		public async Task<IActionResult> LogOff()
		{
			
			await _signInManager.SignOutAsync();
			return Redirect("/home/index");
		}
	}
}
