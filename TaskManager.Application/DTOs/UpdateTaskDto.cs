using System.ComponentModel.DataAnnotations;
using TaskPriority = TaskManager.Domain.Enums.TaskPriority;
using TaskStatus = TaskManager.Domain.Enums.TaskStatus;

namespace TaskManager.Application.DTOs;

public class UpdateTaskDto
{
    [Required]
    [StringLength(200)]
    public required string Title { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }

    [EnumDataType(typeof(TaskPriority))]
    public TaskPriority Priority { get; set; }

    [EnumDataType(typeof(TaskStatus))]
    public TaskStatus Status { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? DueDate { get; set; }

    [Range(1, int.MaxValue)]
    public int UserId { get; set; }
}
