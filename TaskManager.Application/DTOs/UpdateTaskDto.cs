using TaskManager.Domain.Enums;

namespace TaskManager.Application.DTOs;

public class UpdateTaskDto
{
    public required string Title { get; set; }

    public string? Description { get; set; }

    public TaskPriority Priority { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? DueDate { get; set; }

    public int UserId { get; set; }
}
