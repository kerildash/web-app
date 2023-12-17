using Microsoft.AspNetCore.Mvc;

namespace WebApp.Authorization.Controllers
{
	public class AdminController: Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
