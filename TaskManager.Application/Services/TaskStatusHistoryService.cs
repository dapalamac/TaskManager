using TaskManager.Application.DTOs;
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

    public async Task<IEnumerable<TaskStatusHistoryResponseDto>> GetByTaskIdAsync(
        int taskId,
        CancellationToken cancellationToken)
    {
        var history = await _taskStatusHistoryRepository.GetByTaskIdAsync(
            taskId,
            cancellationToken);

        return history.Select(item => new TaskStatusHistoryResponseDto
        {
            TaskId = item.TaskId,
            OldStatus = item.OldStatus,
            NewStatus = item.NewStatus,
            ChangedAt = item.ChangedAt,
            ChangedByUserId = item.ChangedByUserId
        });
    }

    public Task<TaskStatusHistoryEntity> CreateAsync(
        TaskStatusHistoryEntity taskStatusHistory,
        CancellationToken cancellationToken)
    {
        return _taskStatusHistoryRepository.CreateAsync(taskStatusHistory, cancellationToken);
    }
}
