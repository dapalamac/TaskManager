using TaskManager.Application.DTOs;

namespace TaskManager.Application.Interfaces;

public interface ITaskService
{
    Task<PagedResultDto<TaskResponseDto>> GetAllAsync(
        TaskFilterDto filter,
        CancellationToken cancellationToken);

    Task<TaskResponseDto?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken);

    Task<TaskResponseDto?> CreateAsync(
        CreateTaskDto createTaskDto,
        CancellationToken cancellationToken);

    Task<TaskResponseDto?> UpdateAsync(
        int id,
        UpdateTaskDto updateTaskDto,
        CancellationToken cancellationToken);

    Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken);
}