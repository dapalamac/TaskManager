using System.ComponentModel.DataAnnotations;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.DTOs;

public class CreateTaskDto
{
    [Required]
    [StringLength(200)]
    public required string Title { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }

    [EnumDataType(typeof(TaskPriority))]
    public TaskPriority Priority { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? DueDate { get; set; }

    [Range(1, int.MaxValue)]
    public int UserId { get; set; }
}
