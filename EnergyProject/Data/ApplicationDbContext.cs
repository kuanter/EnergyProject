using EnergyProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;

namespace EnergyProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Address>Addresses { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<CardData>CardDatas { get; set; }
        public DbSet<Meter> Meters { get; set; }
        public DbSet<MeterReading> MeterReadings { get; set; }
        public DbSet<PaymentAccount> PaymentAccounts { get; set; }
        public DbSet<PowerStatus> PowerStatuses { get; set; }
        public DbSet<Tariff> Tariffs { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Client> Clients { get; set; }
        public Admin AdminProfile { get; set; }
        public Client ClientProfile { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string userId => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //User = PaymentAccount 
            modelBuilder.Entity<PaymentAccount>()
                .HasOne(p => p.User)
                .WithMany(u => u.PaymentAccounts)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // PaymentAccount = Meter
            modelBuilder.Entity<PaymentAccount>()
                .HasOne(p => p.Meter)
                .WithOne(m => m.PaymentAccount)
                .HasForeignKey<Meter>(p => p.PaymentAccountId);

            // Meter = MeterReading
            modelBuilder.Entity<MeterReading>()
                .HasOne(m => m.Meter)
                .WithMany(mr => mr.MeterReadings)
                .HasForeignKey(m => m.MeterId)
                .OnDelete(DeleteBehavior.Restrict);

            // PaymentAccount = Bill
            modelBuilder.Entity<Bill>()
                .HasOne(b => b.PaymentAccount)
                .WithMany(p => p.Bills)
                .HasForeignKey(b => b.PaymentAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            // PaymentAccount = Tariff
            modelBuilder.Entity<PaymentAccount>()
                .HasOne(p => p.Tariff)
                .WithMany(t => t.PaymentAccounts)
                .HasForeignKey(p => p.TariffId)
                .OnDelete(DeleteBehavior.Restrict);

            // PaymentAccount = PowerStatus
            modelBuilder.Entity<PaymentAccount>()
                 .HasOne(p => p.PowerStatus)
                 .WithMany(pps => pps.PaymentAccounts)
                 .HasForeignKey(p => p.PowerStatusId)
                 .OnDelete(DeleteBehavior.Restrict);

            //Bill = CardData
            modelBuilder.Entity<Bill>()
                .HasOne(b => b.CardData)
                .WithMany(c => c.Bills)
                .HasForeignKey(b => b.CardDataId)
                .OnDelete(DeleteBehavior.Restrict);


            // Address = CardData 
            modelBuilder.Entity<CardData>()
                .HasOne(a => a.Address)
                .WithMany(c => c.CardDatas)
                .HasForeignKey(a => a.AddressId)
                .OnDelete(DeleteBehavior.Restrict);
           

            //User = CardData 
            modelBuilder.Entity<CardData>()
                .HasOne(c => c.User)
                .WithMany(u => u.Cards)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            //PaymentAccound = Address 
            modelBuilder.Entity<PaymentAccount>()
                .HasOne(p => p.Address)
                .WithOne(a => a.PaymentAccount)
                .HasForeignKey<Address>(a => a.PaymentAccountId);
            


            modelBuilder.Entity<Client>()
                .HasOne(c => c.User)
                .WithOne()
                .HasForeignKey<Client>(c => c.UserId);

            modelBuilder.Entity<Admin>()
                .HasOne(a => a.User)
                .WithOne()
                .HasForeignKey<Admin>(a => a.UserId);

            /* modelBuilder.Entity<IdentityUserLogin<string>>()
                 .HasKey(l => new { l.LoginProvider, l.ProviderKey });

             modelBuilder.Entity<IdentityUserRole<string>>()
                 .HasKey(r => new { r.UserId, r.RoleId });

             modelBuilder.Entity<IdentityUserToken<string>>()
                 .HasKey(t => t.UserId);*/

            modelBuilder.Entity<PaymentAccount>()
              .HasQueryFilter(be =>
                  be.UserId == userId);

        }
    }
}
