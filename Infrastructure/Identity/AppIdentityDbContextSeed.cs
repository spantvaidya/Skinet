using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Sameer",
                    Email = "sameer.pantvaidya@gmail.com",
                    UserName = "",
                    Address = new Address
                    {
                        FirstName = "Sameer",
                        LastName = "P",
                        Street = "Bavdhan",
                        City = "Pune",
                        State = "Maharashtra",
                        ZipCode = "411021"
                    }
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}