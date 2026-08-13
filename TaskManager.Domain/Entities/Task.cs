using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Entities;

public class Task
{
    public int Id { get; set; }

    public required string Title { get; set; }

    public string? Description { get; set; }

    public TaskPriority Priority { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? CompletedAt { get; set; }

    public DateTime? DueDate { get; set; }

    public TaskManager.Domain.Enums.TaskStatus Status { get; set; }

    public int UserId { get; set; }
}
