using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Queue_WebApi.Models;

namespace Queue_WebApi.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(u => u.Group)
                .WithMany()
                .HasForeignKey(u => u.GroupId)
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }

}
