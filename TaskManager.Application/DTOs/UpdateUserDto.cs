using System.ComponentModel.DataAnnotations;

namespace TaskManager.Application.DTOs;

public class UpdateUserDto
{
    [Required]
    [StringLength(200)]
    public required string Name { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(320)]
    public required string Email { get; set; }
}
