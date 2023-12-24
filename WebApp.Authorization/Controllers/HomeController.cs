using Microsoft.AspNetCore.Mvc;

namespace WebApp.Authorization.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			ViewBag.Name = User.Identity.Name;
			ViewBag.IsAuthenticated = User.Identity.IsAuthenticated;

			return View();
		}
	}
}
