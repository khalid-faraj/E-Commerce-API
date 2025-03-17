using Core.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Identity
{
	public class AppIdentityDbContextSeed
	{
		public static async Task SeedUserAsync(UserManager<AppUser> userManager)
		{
			if(!userManager.Users.Any())
			{
				var user = new AppUser
				{
					DisplayName = "Khalid",
					Email = "khalid@gmail.com",
					UserName = "khalid@gmail.com",
					Address = new Address
					{
						FirstName = "Khalid",
						LastName = "Faraj",
						Street = "kfj",
						City = "PF",
						State = "EG",
						ZipCode = "12345"
					}
				};
				await userManager.CreateAsync(user, "P@ss123");
			}
		}
	}
}
