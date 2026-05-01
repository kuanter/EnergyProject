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
                await SeedTariffsAsync(db);
                await SeedPowerStatusesAsync(db);
                await SeedAddressesAsync(db);
                await db.SaveChangesAsync();

             
                await SeedAdminAsync(userManager);
                await SeedClientAsync(userManager, db);
                await db.SaveChangesAsync();

             
                await SeedPaymentAccountsAsync(db, userManager);
                await db.SaveChangesAsync();

              
                await SeedMetersAsync(db);
                await SeedCardDatasAsync(db, userManager);
                await db.SaveChangesAsync();

                
                await SeedMeterReadingsAsync(db);
                await SeedBillsAsync(db);
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

        private static async Task SeedClientAsync(UserManager<User> userManager, ApplicationDbContext db)
        {
            User? user = await userManager.FindByEmailAsync("client@gmail.com");

            if (user == null)
            {
                user = new User("client@gmail.com", "client@gmail.com", true);

                var result = await userManager.CreateAsync(user, "1Qwer$");
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }

            if (!await userManager.IsInRoleAsync(user, "Client"))
            {
                await userManager.AddToRoleAsync(user, "Client");
            }

            if (!await db.Set<Client>().AnyAsync(c => c.UserId == user.Id))
            {
                db.Set<Client>().Add(new Client(user.Id));
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
        private static async Task SeedPaymentAccountsAsync(ApplicationDbContext db, UserManager<User> userManager)
        {
            var client = await userManager.FindByEmailAsync("client@gmail.com");
           
            var address = await db.Addresses.FirstOrDefaultAsync(a => a.City == "Bratislava" && a.PaymentAccountId == null);

            var tariff = await db.Tariffs.FirstOrDefaultAsync(t => t.Name == "Standard");
            var powerStatus = await db.PowerStatuses.FirstOrDefaultAsync(p => p.Status == "Active");

            if (client != null && address != null && tariff != null && powerStatus != null)
            {
                if (!await db.PaymentAccounts.AnyAsync(pa => pa.UserId == client.Id))
                {
                    var paymentAccount = new PaymentAccount(
                        client.Id,
                        address.Id,
                        tariff.Id,
                        powerStatus.Id,
                        null
                    );

                    db.PaymentAccounts.Add(paymentAccount);
                
                    address.PaymentAccountId = paymentAccount.Id;
                }
            }
        }

        private static async Task SeedMetersAsync(ApplicationDbContext db)
        {
            var account = await db.PaymentAccounts.FirstOrDefaultAsync();

            if (account != null)
            {
                if (!await db.Set<Meter>().AnyAsync(m => m.PaymentAccountId == account.Id))
                {
                    var meter = new Meter("SN-987654321", account.Id);
                    meter.IsActive = true;
                    db.Set<Meter>().Add(meter);

                    account.MeterId = meter.Id; 
                }
            }
        }

        private static async Task SeedMeterReadingsAsync(ApplicationDbContext db)
        {
            var meter = await db.Set<Meter>().FirstOrDefaultAsync();

            if (meter != null)
            {
                if (!await db.Set<MeterReading>().AnyAsync(mr => mr.MeterId == meter.Id))
                {
                    db.Set<MeterReading>().Add(new MeterReading(150.5f, meter.Id) { CreatedAt = DateTime.Now.AddMonths(-2) });
                    db.Set<MeterReading>().Add(new MeterReading(310.2f, meter.Id) { CreatedAt = DateTime.Now.AddMonths(-1) });
                }
            }
        }

        private static async Task SeedCardDatasAsync(ApplicationDbContext db, UserManager<User> userManager)
        {
            var client = await userManager.FindByEmailAsync("client@gmail.com");
            var address = await db.Addresses.FirstOrDefaultAsync();

            if (client != null && address != null)
            {
                if (!await db.Set<CardData>().AnyAsync(cd => cd.UserId == client.Id))
                {
                    db.Set<CardData>().Add(
                        new CardData(4111111111111111, 12, 2028, "JOHN DOE", true, address.Id, client.Id)
                    );
                }
            }
        }

        private static async Task SeedBillsAsync(ApplicationDbContext db)
        {
            var account = await db.PaymentAccounts.FirstOrDefaultAsync();
            var card = await db.Set<CardData>().FirstOrDefaultAsync();

            if (account != null)
            {
                if (!await db.Set<Bill>().AnyAsync(b => b.PaymentAccountId == account.Id))
                {
               
                    db.Set<Bill>().Add(new Bill(
                        159.7f,
                        718.65f,
                        "Paid",
                        DateTime.Now.AddMonths(-1),
                        account.Id,
                        card?.Id
                    ));

                    db.Set<Bill>().Add(new Bill(
                        120.0f,
                        540.0f,
                        "Pending",
                        DateTime.Now,
                        account.Id,
                        null
                    ));
                }
            }
        }

    }
}
