using EnergyProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EnergyProject.Data
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
                if (await userManager.FindByEmailAsync("admin@gmail.com") != null)
                {
                    Console.WriteLine("Basic data already exists");
                    return;
                }

                List <string>Roles = new List<string> { "Admin", "Client" };

                foreach (string role in Roles)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }

                User user = new User();
                user.Email = "admin@gmail.com";
                user.UserName = "admin@gmail.com";
                user.EmailConfirmed = true;

                var result = await userManager.CreateAsync(user,"Admin!123");
                await userManager.AddToRoleAsync(user, "Admin");

                // Tariffs
                if (!await db.Tariffs.AnyAsync(t => t.Name == "Standard"))
                {
                    db.Tariffs.Add(new Tariff
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Standard",
                        PricePerKWh = 4.5f
                    });
                }

                if (!await db.Tariffs.AnyAsync(t => t.Name == "Night"))
                {
                    db.Tariffs.Add(new Tariff
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Night",
                        PricePerKWh = 2.8f
                    });
                }

                if (!await db.Tariffs.AnyAsync(t => t.Name == "Business"))
                {
                    db.Tariffs.Add(new Tariff
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Business",
                        PricePerKWh = 5.2f
                    });
                }

                // Addresses
                if (!await db.Addresses.AnyAsync(a =>
                    a.City == "Bratislava" &&
                    a.Street == "Main Street" &&
                    a.House == "12" &&
                    a.Apartment == "5"))
                {
                    db.Addresses.Add(new Address
                    {
                        Id = Guid.NewGuid().ToString(),
                        City = "Bratislava",
                        Street = "Main Street",
                        House = "12",
                        Apartment = "5",
                        PaymentAccountId = null
                    });
                }

                if (!await db.Addresses.AnyAsync(a =>
                    a.City == "Kosice" &&
                    a.Street == "Green Avenue" &&
                    a.House == "45" &&
                    a.Apartment == "11"))
                {
                    db.Addresses.Add(new Address
                    {
                        Id = Guid.NewGuid().ToString(),
                        City = "Kosice",
                        Street = "Green Avenue",
                        House = "45",
                        Apartment = "11",
                        PaymentAccountId = null
                    });
                }

                if (!await db.Addresses.AnyAsync(a =>
                    a.City == "Zilina" &&
                    a.Street == "River Road" &&
                    a.House == "8" &&
                    a.Apartment == "2"))
                {
                    db.Addresses.Add(new Address
                    {
                        Id = Guid.NewGuid().ToString(),
                        City = "Zilina",
                        Street = "River Road",
                        House = "8",
                        Apartment = "2",
                        PaymentAccountId = null
                    });
                }


                await db.SaveChangesAsync();

                Console.WriteLine("Seeding completed successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to seed data {ex.Message}");
                return;

            }



        }
    }
}
