using System.ComponentModel.DataAnnotations;
using TaskManager.Domain.Enums;
using TaskStatus = TaskManager.Domain.Enums.TaskStatus;

namespace TaskManager.Application.DTOs;

public class UpdateTaskDto
{
    [Required(ErrorMessage = "El título es obligatorio.")]
    [MaxLength(200, ErrorMessage = "El título no puede superar los 200 caracteres.")]
    public string Title { get; set; } = string.Empty;

    [MaxLength(500, ErrorMessage = "La descripción no puede superar los 500 caracteres.")]
    public string? Description { get; set; }

    public TaskPriority Priority { get; set; }

    public TaskStatus Status { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? DueDate { get; set; }

    [Required(ErrorMessage = "El usuario responsable es obligatorio.")]
    public int UserId { get; set; }
}