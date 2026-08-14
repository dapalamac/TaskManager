using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskEntity = TaskManager.Domain.Entities.Task;
using UserEntity = TaskManager.Domain.Entities.User;

namespace TaskManager.Infrastructure.Data.Configurations;

public class TaskConfiguration : IEntityTypeConfiguration<TaskEntity>
{
    public void Configure(EntityTypeBuilder<TaskEntity> builder)
    {
        builder.HasKey(task => task.Id);

        builder.ToTable("Tasks", table =>
            table.UseSqlOutputClause(false));

        builder.Property(task => task.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(task => task.Description)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.Property(task => task.Priority)
            .HasConversion<int>();

        builder.Property(task => task.Status)
            .HasConversion<int>();

        builder.Property(task => task.CreatedAt)
            .IsRequired();

        builder.Property(task => task.StartDate)
            .IsRequired(false);

        builder.Property(task => task.DueDate)
            .IsRequired(false);

        builder.Property(task => task.CompletedAt)
            .IsRequired(false);

        builder.Property(task => task.UserId)
            .IsRequired();

        builder.HasOne<UserEntity>()
            .WithMany()
            .HasForeignKey(task => task.UserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
