using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Entities;

public class TaskStatusHistory
{
    public int Id { get; set; }

    public int TaskId { get; set; }

    public TaskManager.Domain.Enums.TaskStatus OldStatus { get; set; }

    public TaskManager.Domain.Enums.TaskStatus NewStatus { get; set; }

    public DateTime ChangedAt { get; set; }

    public int ChangedByUserId { get; set; }
}
