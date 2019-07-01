using Dvik.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Dvik.Areas.Identity
{
    public static class SeedData
    {
        private const string roleName = "Administrator";
        private const string adminEmail = "administrator@dvik.ru";
        private const string adminPassword = "N8l5%L3S^KU8";

        public static void SeedUsersAndRole(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var provider = scope.ServiceProvider;
                var context = provider.GetRequiredService<DvikDbContext>();
                var userManager = provider.GetRequiredService<UserManager<IdentityUser>>();
                var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();

                // automigration 
                //context.Database.Migrate();
                InstallUsers(userManager, roleManager);
            }
        }

        private static void InstallUsers(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var roleExist = roleManager.RoleExistsAsync(roleName).Result;
            if (!roleExist)
            {
                //create the roles and seed them to the database
                roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
            }

            var user = userManager.FindByNameAsync(adminEmail).Result;

            if (user != null)
            {
                return;
            }

            var serviceUser = new IdentityUser
            {
                UserName = adminEmail,
                Email = adminEmail
            };

            var createAdminUser = userManager.CreateAsync(serviceUser, adminPassword).Result;

            if (!createAdminUser.Succeeded)
            {
                return;
            }

            var confirmationToken = userManager.GenerateEmailConfirmationTokenAsync(serviceUser).Result;
            var result = userManager.ConfirmEmailAsync(serviceUser, confirmationToken).Result;
            //here we tie the new user to the role
            userManager.AddToRoleAsync(serviceUser, roleName).GetAwaiter().GetResult();
        }
    }
}
