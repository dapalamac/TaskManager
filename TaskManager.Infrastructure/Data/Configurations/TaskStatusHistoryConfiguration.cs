using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskEntity = TaskManager.Domain.Entities.Task;
using TaskStatusHistoryEntity = TaskManager.Domain.Entities.TaskStatusHistory;
using UserEntity = TaskManager.Domain.Entities.User;

namespace TaskManager.Infrastructure.Data.Configurations;

public class TaskStatusHistoryConfiguration : IEntityTypeConfiguration<TaskStatusHistoryEntity>
{
    public void Configure(EntityTypeBuilder<TaskStatusHistoryEntity> builder)
    {
        builder.HasKey(history => history.Id);

        builder.Property(history => history.OldStatus)
            .HasConversion<int>();

        builder.Property(history => history.NewStatus)
            .HasConversion<int>();

        builder.Property(history => history.ChangedAt)
            .IsRequired();

        builder.Property(history => history.TaskId)
            .IsRequired();

        builder.Property(history => history.ChangedByUserId)
            .IsRequired();

        builder.HasOne<TaskEntity>()
            .WithMany()
            .HasForeignKey(history => history.TaskId);

        builder.HasOne<UserEntity>()
            .WithMany()
            .HasForeignKey(history => history.ChangedByUserId);
    }
}
