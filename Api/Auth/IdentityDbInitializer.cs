using Microsoft.AspNetCore.Identity;

namespace LinkDev.EgyptianRecipes.Auth;

public static class IdentityDbInitializer
{
    public static void SeedUsers(UserManager<IdentityUser> userManager)
    {
        if (userManager.FindByEmailAsync("admin@gmail.com").Result==null)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com"
            };

            IdentityResult result = userManager.CreateAsync(user, "123456").Result;

            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user, "Admin").Wait();
            }
        }       
    }   
}
