using TaskManager.Domain.Enums;
using TaskStatus = TaskManager.Domain.Enums.TaskStatus;

namespace TaskManager.Application.DTOs;

public class TaskFilterDto
{
    public TaskPriority? Priority { get; set; }

    public TaskStatus? Status { get; set; }

    public int? UserId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 20;
}