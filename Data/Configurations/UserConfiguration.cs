using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Api.Models.Entities;

namespace UserManagement.Api.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            
            builder.HasKey(u => u.Id);
            
            builder.Property(u => u.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();
                
            builder.Property(u => u.FirstName)
                .HasColumnName("first_name")
                .IsRequired()
                .HasMaxLength(100);
                
            builder.Property(u => u.LastName)
                .HasColumnName("last_name")
                .IsRequired()
                .HasMaxLength(100);
                
            builder.Property(u => u.Email)
                .HasColumnName("email")
                .IsRequired()
                .HasMaxLength(200);
                
            builder.Property(u => u.Address)
                .HasColumnName("address")
                .HasMaxLength(500);
                
            builder.Property(u => u.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();
                
            builder.Property(u => u.UpdatedAt)
                .HasColumnName("updated_at")
                .IsRequired();

            builder.HasIndex(u => u.Email)
                .IsUnique()
                .HasDatabaseName("IX_Users_Email");
        }
    }
}