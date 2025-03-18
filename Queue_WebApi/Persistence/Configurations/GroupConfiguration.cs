using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Queue_WebApi.Models;

namespace Queue_WebApi.Persistence.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasKey(g => g.Id);
            builder.Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(100);
             
            builder.HasMany(g => g.Users)
                .WithOne(u => u.Group)
                .HasForeignKey(u => u.GroupId)
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }

}
