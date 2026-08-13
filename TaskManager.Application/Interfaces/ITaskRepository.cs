using TaskEntity = TaskManager.Domain.Entities.Task;

namespace TaskManager.Application.Interfaces;

public interface ITaskRepository
{
    Task<TaskEntity?> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task<IEnumerable<TaskEntity>> GetAllAsync(CancellationToken cancellationToken);

    Task<TaskEntity> CreateAsync(TaskEntity task, CancellationToken cancellationToken);

    Task UpdateAsync(TaskEntity task, CancellationToken cancellationToken);

    Task DeleteAsync(int id, CancellationToken cancellationToken);
}
