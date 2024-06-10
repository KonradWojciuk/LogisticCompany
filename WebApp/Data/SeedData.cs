using Microsoft.AspNetCore.Identity;

namespace WebApp.Data
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roleNames = { "Admin", "User" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var adminUser = new IdentityUser
            {
                UserName = "admin",
                Email = "admin@example.com",
            };

            string userPWD = "Admin@123";
            var _user = await userManager.FindByEmailAsync("admin@example.com");

            if (_user == null)
            {
                var createPowerUser = await userManager.CreateAsync(adminUser, userPWD);
                if (createPowerUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
