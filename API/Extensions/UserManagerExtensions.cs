using System.Security.Claims;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<AppUser> FindUserByClaimsWithAddress(this UserManager<AppUser> userManager,
        ClaimsPrincipal user)
        {
            var email = user.FindFirstValue(ClaimTypes.Email);

            return await userManager.Users.Include(x => x.Address)
            .SingleOrDefaultAsync(e => e.Email == email);
        }

        public static async Task<AppUser> FindEmailByClaims(this UserManager<AppUser> userManager,
        ClaimsPrincipal user)
        {
            return await userManager.Users
            .SingleOrDefaultAsync(e => e.Email == user.FindFirstValue(ClaimTypes.Email));
        }
    }
}