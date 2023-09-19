using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Auth;
using Microsoft.AspNetCore.Identity;

namespace ResolverQuestao.Services
{
    public class SeedUserInitial : ISeedUserInitial
    {

        private readonly UserManager<IdentityUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedUserInitial(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }





        public async Task SeedRolesAsync()
        {
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                var role = new IdentityRole("Admin")
                {
                    Name = "Admin",

                    NormalizedName = "ADMIN",

                    ConcurrencyStamp = Guid.NewGuid().ToString()
                };


                await _roleManager.CreateAsync(role);
            }

            //user

            if (!await _roleManager.RoleExistsAsync("User"))
            {
                var role = new IdentityRole("User")
                {
                    Name = "User",

                    NormalizedName = "USER",

                    ConcurrencyStamp = Guid.NewGuid().ToString()
                };


                await _roleManager.CreateAsync(role);

            }

            //super user

            if (!await _roleManager.RoleExistsAsync("SuperUser"))
            {
                var role = new IdentityRole("SuperUser")
                {
                    Name = "SuperUser",

                    NormalizedName = "SUPERUSER",

                    ConcurrencyStamp = Guid.NewGuid().ToString()
                };

                await _roleManager.CreateAsync(role);
            }

        }

        public async Task SeedUsersAsync()
        {

            //procurar todos os usuarios e atribuir a marca de User

            // var users = _userManager.Users.ToList();

            // foreach (var user in users)
            // {
            //     if (await _userManager.IsInRoleAsync(user, "User"))
            //     {
            //         continue;
            //     }
            //     else
            //     {
            //         await _userManager.AddToRoleAsync(user, "User");
            //     }
            // }


            // if (await _userManager.FindByEmailAsync(" vitinholol@vitinholol ") == null)

            // {
            //     //criar um novo usuario

            //     var user = new IdentityUser
            //     {
            //         UserName = "vitinholol@vitinholol",

            //         Email = "vitinholol@vitinholol",

            //         EmailConfirmed = true,
            //         NormalizedEmail = "vitinholol@vitinholol".ToUpper(),

            //         NormalizedUserName = "vitinholol@vitinholol".ToUpper(),

            //         LockoutEnabled = true,

            //         SecurityStamp = Guid.NewGuid().ToString()
            //     };

            //     //criar o usuario com a senha Vitinholol5@

            //     var result = await _userManager.CreateAsync(user, "Vitinholol5@");

            //     if (result.Succeeded)
            //     {
            //         await _userManager.AddToRoleAsync(user, "Admin");
            //     }

            // }

        }
    }
}