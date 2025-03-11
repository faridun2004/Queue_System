using Microsoft.EntityFrameworkCore;
using Queue_WebApi.Models;
using Queue_WebApi.Persistence.Configurations;

namespace Queue_WebApi.Persistence.DataBase
{
    public class QueueDbContext : DbContext
    {
        public QueueDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Apointment> Apointsments { get; set; }
        public DbSet<QueueItem> QueueItems { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppointmentConfiguration());
            modelBuilder.ApplyConfiguration(new HallConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new GroupConfiguration());
            modelBuilder.ApplyConfiguration(new QueueItemConfiguration());


        }
    }
}
