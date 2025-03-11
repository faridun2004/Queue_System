using Appointment_Hall.Models;
using Microsoft.EntityFrameworkCore;

namespace Appointment_Hall.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Hall> Halls { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Client> Clients { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Client>()
        .HasMany(c => c.Reservations)
        .WithOne(r => r.Client)
        .HasForeignKey(r => r.ClientId);

            modelBuilder.Entity<Hall>()
                .HasMany(h => h.Reservations)
                .WithOne(r => r.Hall)
                .HasForeignKey(r => r.HallId);
        }
    }
}
