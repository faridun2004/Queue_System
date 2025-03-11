using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Queue_WebApi.Models;

namespace Queue_WebApi.Persistence.Configurations
{
    public class QueueItemConfiguration : IEntityTypeConfiguration<QueueItem>
    {
        public void Configure(EntityTypeBuilder<QueueItem> builder)
        {
            builder.HasKey(q => q.Id);

            builder.Property(q => q.StartTime)
                .IsRequired();

            builder.Property(q => q.EndTime)
                .IsRequired();


            builder.HasOne(q => q.Hall)
                .WithMany()
                .HasForeignKey(q => q.HallId)
                .OnDelete(DeleteBehavior.Restrict); // зал не удалится при удалении очереди

            builder.HasOne(q => q.User)
                .WithMany()
                .HasForeignKey(q => q.UserId)
                .OnDelete(DeleteBehavior.Restrict); // пользователь не удалится при удалении очереди
        }
    }

}
