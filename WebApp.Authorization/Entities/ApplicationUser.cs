using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Authorization.Entities
{

	public class ApplicationUser : IdentityUser<Guid>
	{
		
		//[Column("id")]
		//[Key]
		//public Guid Id { get; set; }

		//[Column("password")]
		//public string Password { get; set; }

		//[Column("username")]
		//public string UserName { get; set; }

	
		public string LastName { get; set; }
	}
}
