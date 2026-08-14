using TaskManager.Application.DTOs;
using TaskEntity = TaskManager.Domain.Entities.Task;

namespace TaskManager.Application.Interfaces;

public interface ITaskRepository
{
    Task<TaskEntity?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken);

    Task<IEnumerable<TaskEntity>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<TaskEntity> CreateAsync(
        TaskEntity task,
        CancellationToken cancellationToken);

    Task UpdateAsync(
        TaskEntity task,
        CancellationToken cancellationToken);

    Task DeleteAsync(
        int id,
        CancellationToken cancellationToken);

    Task<(IReadOnlyList<TaskEntity> Items, int TotalItems)> GetPagedAsync(
        TaskFilterDto filter,
        CancellationToken cancellationToken);

    Task<bool> ExistsByTitleAndUserIdAsync(
    string title,
    int userId,
    int? excludeTaskId,
    CancellationToken cancellationToken);
}