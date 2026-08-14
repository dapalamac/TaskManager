using TaskManager.Application.DTOs;
using TaskStatusHistoryEntity = TaskManager.Domain.Entities.TaskStatusHistory;

namespace TaskManager.Application.Interfaces;

public interface ITaskStatusHistoryService
{
    Task<IEnumerable<TaskStatusHistoryResponseDto>> GetByTaskIdAsync(
        int taskId,
        CancellationToken cancellationToken);

    Task<TaskStatusHistoryEntity> CreateAsync(
        TaskStatusHistoryEntity taskStatusHistory,
        CancellationToken cancellationToken);
}
