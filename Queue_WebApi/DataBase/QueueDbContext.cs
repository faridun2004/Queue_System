using Microsoft.EntityFrameworkCore;
using Queue_WebApi.Models;

namespace Queue_WebApi.DataBase
{
    public class QueueDbContext: DbContext
    {
        public QueueDbContext(DbContextOptions options): base(options) { }  
        public DbSet<Apointment> Apointsments { get; set; }
        public DbSet<Queue> Queues { get; set; }
        public DbSet<Worker> Workers { get; set; }  
        public DbSet<Client>  Clients { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<Queue> Queueas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Queue>()
       .HasMany(q => q.Apointments)
       .WithOne(a => a.Queue)
       .HasForeignKey(a => a.QueueId) 
       .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Queue>()
                .HasOne(q => q.Hall)
                .WithMany()
                .HasForeignKey(q => q.HallId);
        }
    }
}
