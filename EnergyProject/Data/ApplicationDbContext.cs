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

            /* Bill = CardData
            modelBuilder.Entity<Bill>()
                .HasOne(b => b.CardData)
                .WithOne(c => c.Bill)
                .HasForeignKey<CardData>(b => b.BillId);
            */
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
        }
    }
}
