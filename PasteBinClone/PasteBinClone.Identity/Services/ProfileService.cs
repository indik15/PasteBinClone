using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Validation;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using PasteBinClone.Identity.Models;
using System.Security.Claims;

namespace PasteBinClone.Identity.Services
{
    public class ProfileService(UserManager<AppUser> userManager) : IProfileService
    {
        //instance used for user management
        private readonly UserManager<AppUser> _userManager = userManager;

        //The method that is called to receive additional claims
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);

            List<Claim> claims = new();

            //Get user roles
            var roles = await _userManager.GetRolesAsync(user);

            //Added claims with roles and user name
            claims.AddRange(roles.SelectMany(role => new[]
            {
                new Claim(JwtClaimTypes.Role, role),
                new Claim(JwtClaimTypes.Name, user.UserName)

            }));

            //Sets received claims in context
            context.IssuedClaims = claims;
        }

        //The method that is called to check for user activity
        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);
            context.IsActive = user != null;
        }
    }

}
