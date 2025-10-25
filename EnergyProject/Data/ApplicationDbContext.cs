using EnergyProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EnergyProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Address>Addresses { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<CardData>CardDatas { get; set; }
        public DbSet<Meter> Meters { get; set; }
        public DbSet<MeterReading> MeterReadings { get; set; }
        public DbSet<PaymentAccount> PaymentAccounts { get; set; }
        public DbSet<PowerStatus> PowerStatuses { get; set; }
        public DbSet<Tariff> Tariffs { get; set; }
        public DbSet<User> Users { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //User = PaymentAccount 
            modelBuilder.Entity<PaymentAccount>()
                .HasOne(p => p.User)
                .WithMany(u => u.PaymentAccounts)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // PaymentAccount = PowerStatus
            modelBuilder.Entity<PaymentAccount>()
                 .HasOne(p => p.PowerStatus)
                 .WithOne(pps => pps.PaymentAccount)
               .HasForeignKey<PowerStatus>(p => p.PaymentAccountId);

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
                .WithOne(t => t.PaymentAccount)
                .HasForeignKey<Tariff>(p => p.TariffId);

            // Bill = CardData
            modelBuilder.Entity<Bill>()
                .HasOne(b => b.CardData)
                .WithOne(c => c.Bill)
                .HasForeignKey<CardData>(b => b.BillId);

            // Address = CardData 
            modelBuilder.Entity<Address>()
                .HasOne(a => a.CardData)
                .WithOne(c => c.Address)
                .HasForeignKey<CardData>(a => a.AddressId);

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
        }
    }
}
