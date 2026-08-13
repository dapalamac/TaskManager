using TaskManager.Application.Interfaces;
using TaskStatusHistoryEntity = TaskManager.Domain.Entities.TaskStatusHistory;

namespace TaskManager.Application.Services;

public class TaskStatusHistoryService : ITaskStatusHistoryService
{
    private readonly ITaskStatusHistoryRepository _taskStatusHistoryRepository;

    public TaskStatusHistoryService(ITaskStatusHistoryRepository taskStatusHistoryRepository)
    {
        _taskStatusHistoryRepository = taskStatusHistoryRepository;
    }

    public Task<IEnumerable<TaskStatusHistoryEntity>> GetByTaskIdAsync(
        int taskId,
        CancellationToken cancellationToken)
    {
        return _taskStatusHistoryRepository.GetByTaskIdAsync(taskId, cancellationToken);
    }

    public Task<TaskStatusHistoryEntity> CreateAsync(
        TaskStatusHistoryEntity taskStatusHistory,
        CancellationToken cancellationToken)
    {
        return _taskStatusHistoryRepository.CreateAsync(taskStatusHistory, cancellationToken);
    }
}
