using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserEntity = TaskManager.Domain.Entities.User;

namespace TaskManager.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(user => user.Id);

        builder.Property(user => user.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(user => user.Email)
            .IsRequired()
            .HasMaxLength(320);

        builder.HasIndex(user => user.Email)
            .IsUnique();
    }
}
