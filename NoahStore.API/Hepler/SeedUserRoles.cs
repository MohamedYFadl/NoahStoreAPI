using Microsoft.AspNetCore.Identity;
using NoahStore.Core.Entities.Identity;

namespace NoahStore.API.Hepler
{
    public  class SeedUserRoles
    {
        public static async Task SeedUserAndRolesAsync(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var userManager = services.GetRequiredService<UserManager<AppUser>>();
            var user = new AppUser()
            {
                DisplayName = "Mohamed Yasser",
                Email = "Yasovic03@gmail.com",
                EmailConfirmed = true,
                Address = new Address()
                {
                    FirstName = "Mohamed",
                    LastName = "Yasser",
                    City = "Alex",
                    State = "EG",
                    Street = "Salah Salem",
                    ZipCode = "2567",
                },
                UserName = "momoYasovic"
            };
            if(!userManager.Users.Any())
            {
              var result =  await userManager.CreateAsync(user, "0105140!Ma");
            }
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { "Admin", "Customer", "Company", "Employee" };
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            await userManager.AddToRoleAsync(user, "Admin");

        }

    }
}
