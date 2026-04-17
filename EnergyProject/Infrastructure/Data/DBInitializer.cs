using EnergyProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EnergyProject.Infrastructure.Data
{
    public static class DBInitializer
    {
        public static async Task SeedAsync(this IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            var db = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            try
            {
                await SeedRolesAsync(roleManager);
                await SeedAdminAsync(userManager);
                await SeedTariffsAsync(db);
                await SeedAddressesAsync(db);
                await SeedPowerStatusesAsync(db);

                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to seed data {ex.Message}");
                return;
            }
        }

        private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            List<string> roles = new List<string> { "Admin", "Client" };

            foreach (string role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        private static async Task SeedAdminAsync(UserManager<User> userManager)
        {
            User? user = await userManager.FindByEmailAsync("admin@gmail.com");

            if (user == null)
            {
                user = new User("admin@gmail.com", "admin@gmail.com", true);

                var result = await userManager.CreateAsync(user, "1Q");
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }

            if (!await userManager.IsInRoleAsync(user, "Admin"))
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }

        private static async Task SeedTariffsAsync(ApplicationDbContext db)
        {
            if (!await db.Tariffs.AnyAsync(t => t.Name == "Standard"))
            {
                db.Tariffs.Add(
                    new Tariff( "Standard", 4.5f)
                );
            }

            if (!await db.Tariffs.AnyAsync(t => t.Name == "Night"))
            {
                db.Tariffs.Add(
                    new Tariff( "Night", 2.8f)
                );
            }

            if (!await db.Tariffs.AnyAsync(t => t.Name == "Business"))
            {
                db.Tariffs.Add(
                    new Tariff( "Business", 5.2f)
                );
            }
        }

        private static async Task SeedAddressesAsync(ApplicationDbContext db)
        {
            if (!await db.Addresses.AnyAsync(a =>
                a.City == "Bratislava" &&
                a.Street == "Main Street" &&
                a.House == "12" &&
                a.Apartment == "5"))
            {
                db.Addresses.Add(
                    new Address(
                        "Bratislava",
                        "Main Street",
                        "12",
                        "5",
                        null
                    )
                );
            }

            if (!await db.Addresses.AnyAsync(a =>
                a.City == "Kosice" &&
                a.Street == "Green Avenue" &&
                a.House == "45" &&
                a.Apartment == "11"))
            {
                db.Addresses.Add(
                    new Address(
                        "Kosice",
                        "Green Avenue",
                        "45",
                        "11",
                        null
                    )
                );
            }

            if (!await db.Addresses.AnyAsync(a =>
                a.City == "Zilina" &&
                a.Street == "River Road" &&
                a.House == "8" &&
                a.Apartment == "2"))
            {
                db.Addresses.Add(
                    new Address(
                        "Zilina",
                        "River Road",
                        "8",
                        "2",
                        null
                    )
                );
            }
        }

        private static async Task SeedPowerStatusesAsync(ApplicationDbContext db)
        {
            if (!await db.PowerStatuses.AnyAsync(p => p.Status == "Active"))
            {
                db.PowerStatuses.Add(
                    new PowerStatus(
                        "Active",
                        "Connected and working normally"
                    )
                );
            }

            if (!await db.PowerStatuses.AnyAsync(p => p.Status == "Disconnected"))
            {
                db.PowerStatuses.Add(
                    new PowerStatus(
                        "Disconnected",
                        "Disconnected because of unpaid bills"
                    )
                );
            }

            if (!await db.PowerStatuses.AnyAsync(p => p.Status == "Maintenance"))
            {
                db.PowerStatuses.Add(
                    new PowerStatus(
                        "Maintenance",
                        "Temporarily unavailable due to technical works"
                    )
                );
            }
        }
        
    }
}
