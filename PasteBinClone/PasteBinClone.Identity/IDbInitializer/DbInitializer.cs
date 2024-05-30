﻿using IdentityModel;
using Microsoft.AspNetCore.Identity;
using PasteBinClone.Identity.Data;
using PasteBinClone.Identity.Models;
using System.Security.Claims;

namespace PasteBinClone.Identity.IDbInitializer
{
    public class DbInitializer(
        UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager) : IDbInitializer
    {
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;

        public void Initialize()
        {
            if (_roleManager.FindByNameAsync(UserRoles.Admin).Result == null)
            {
                _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin))
                    .GetAwaiter()
                    .GetResult();

                _roleManager.CreateAsync(new IdentityRole(UserRoles.User))
                    .GetAwaiter()
                    .GetResult();
            } 
            else
                return;

            AppUser admin = new()
            {
                UserName = "Admin1",
                Email = "admin1@gmail.com",
                EmailConfirmed = true,
            };

            _userManager.CreateAsync(admin, "123456789987a")
                .GetAwaiter()
                .GetResult();

            _userManager.AddToRoleAsync(admin, UserRoles.Admin)
                .GetAwaiter()
                .GetResult();

            var claims1 = _userManager.AddClaimsAsync(admin, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, admin.UserName),
                new Claim(JwtClaimTypes.Role, UserRoles.Admin)
            }).Result;
        }
    }
}
