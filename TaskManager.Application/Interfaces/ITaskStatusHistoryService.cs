using TaskStatusHistoryEntity = TaskManager.Domain.Entities.TaskStatusHistory;

namespace TaskManager.Application.Interfaces;

public interface ITaskStatusHistoryService
{
    Task<IEnumerable<TaskStatusHistoryEntity>> GetByTaskIdAsync(int taskId, CancellationToken cancellationToken);

    Task<TaskStatusHistoryEntity> CreateAsync(
        TaskStatusHistoryEntity taskStatusHistory,
        CancellationToken cancellationToken);
}
