using TaskStatus = TaskManager.Domain.Enums.TaskStatus;

namespace TaskManager.Application.DTOs;

public class TaskStatusHistoryResponseDto
{
    public int TaskId { get; set; }

    public TaskStatus OldStatus { get; set; }

    public TaskStatus NewStatus { get; set; }

    public DateTime ChangedAt { get; set; }

    public int ChangedByUserId { get; set; }
}
