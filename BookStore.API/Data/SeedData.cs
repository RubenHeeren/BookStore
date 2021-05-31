using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.Data
{
    public static class SeedData
    {
        public async static Task Seed(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await SeedRoles(roleManager);
            await SeedUsers(userManager);
        }

        private async static Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            // .result == await
            if (await roleManager.RoleExistsAsync("Administrator") == false)
            {
                var role = new IdentityRole
                {
                    Name = "Administrator",
                };

                await roleManager.CreateAsync(role);
            }

            if (await roleManager.RoleExistsAsync("Customer") == false)
            {
                var role = new IdentityRole
                {
                    Name = "Customer",
                };

                await roleManager.CreateAsync(role);
            }
        }

        private async static Task SeedUsers(UserManager<IdentityUser> userManager)
        {
            if (await userManager.FindByEmailAsync("admin@bookstore.com") == null)
            {
                var user = new IdentityUser
                {
                    UserName = "admin@bookstore.com",
                    Email = "admin@bookstore.com"
                };

                var result = await userManager.CreateAsync(user, "P@ssword1");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Administrator");
                }
            }

            if (await userManager.FindByEmailAsync("customer1@gmail.com") == null)
            {
                var user = new IdentityUser
                {
                    UserName = "customer1@gmail.com",
                    Email = "customer1@gmail.com"
                };

                var result = await userManager.CreateAsync(user, "P@ssword1");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            if (await userManager.FindByEmailAsync("customer2@gmail.com") == null)
            {
                var user = new IdentityUser
                {
                    UserName = "customer2@gmail.com",
                    Email = "customer2@gmail.com"
                };

                var result = await userManager.CreateAsync(user, "P@ssword1");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }
        }        
    }
}
