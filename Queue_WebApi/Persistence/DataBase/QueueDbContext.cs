using Microsoft.EntityFrameworkCore;
using Queue_WebApi.Models;
using Queue_WebApi.Persistence.Configurations;

namespace Queue_WebApi.Persistence.DataBase
{
    public class QueueDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public QueueDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : base(options) 
        {
            _httpContextAccessor=httpContextAccessor;
        }
    
        public DbSet<Apointment> Apointsments { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(QueueDbContext).Assembly);
            modelBuilder.Entity<Apointment>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<User>().HasQueryFilter(u=> !u.IsDeleted);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellation=default)
        {
            var currentUserId = GetCurrentUserId();
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.Now;
                    entry.Entity.CreatedByUserId = currentUserId;
                    entry.Entity.UpdatedByUserId = null; 
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.Now;
                    entry.Entity.UpdatedByUserId = currentUserId;
                }
            }
            return await base.SaveChangesAsync(cancellation);
        }
        private Guid GetCurrentUserId()
        {
            var token = _httpContextAccessor.HttpContext?.User;
            var userIdClaim = token?.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            return userIdClaim != null ? Guid.Parse(userIdClaim) : Guid.Empty;
        }
    }
}
