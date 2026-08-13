using Microsoft.EntityFrameworkCore;
using TaskEntity = TaskManager.Domain.Entities.Task;
using TaskStatusHistoryEntity = TaskManager.Domain.Entities.TaskStatusHistory;
using UserEntity = TaskManager.Domain.Entities.User;

namespace TaskManager.Infrastructure.Data;

public class TaskManagerDbContext : DbContext
{
    public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options)
        : base(options)
    {
    }

    public DbSet<TaskEntity> Tasks { get; set; }

    public DbSet<UserEntity> Users { get; set; }

    public DbSet<TaskStatusHistoryEntity> TaskStatusHistories { get; set; }
}
