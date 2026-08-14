using TaskManager.Application.DTOs;
using TaskManager.Application.Exceptions;
using TaskManager.Application.Interfaces;
using TaskEntity = TaskManager.Domain.Entities.Task;
using TaskStatus = TaskManager.Domain.Enums.TaskStatus;


namespace TaskManager.Application.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly IUserRepository _userRepository;
    private readonly ITaskStatusHistoryRepository _taskStatusHistoryRepository;

    public TaskService(
        ITaskRepository taskRepository,
        IUserRepository userRepository,
        ITaskStatusHistoryRepository taskStatusHistoryRepository)
    {
        _taskRepository = taskRepository;
        _userRepository = userRepository;
        _taskStatusHistoryRepository = taskStatusHistoryRepository;
    }

    public async Task<TaskResponseDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(id, cancellationToken);

        return task is null ? null : MapToResponseDto(task);
    }

    public async Task<IEnumerable<TaskResponseDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var tasks = await _taskRepository.GetAllAsync(cancellationToken);

        return tasks.Select(MapToResponseDto);
    }

    public async Task<TaskResponseDto?> CreateAsync(
        CreateTaskDto createTaskDto,
        CancellationToken cancellationToken)
    {

        if (createTaskDto.DueDate.HasValue &&
        createTaskDto.DueDate.Value.Date < DateTime.UtcNow.Date)
        {
            throw new BusinessException(
                "La fecha límite no puede ser menor a hoy.");
        }

        var duplicated = await _taskRepository.ExistsByTitleAndUserIdAsync(
            createTaskDto.Title,
            createTaskDto.UserId,
            null,
            cancellationToken);

        if (duplicated)
        {
            throw new BusinessException(
                "Ya existe una tarea con el mismo título para este usuario.");
        }

        var user = await _userRepository.GetByIdAsync(createTaskDto.UserId, cancellationToken);

        if (user is null)
        {
            return null;
        }

        var task = new TaskEntity
        {
            Title = createTaskDto.Title,
            Description = createTaskDto.Description,
            Priority = createTaskDto.Priority,
            CreatedAt = DateTime.UtcNow,
            StartDate = createTaskDto.StartDate,
            DueDate = createTaskDto.DueDate,
            Status = TaskStatus.Pending,
            UserId = createTaskDto.UserId
        };

        var createdTask = await _taskRepository.CreateAsync(task, cancellationToken);

        await _taskStatusHistoryRepository.CreateAsync(
            new()
            {
                TaskId = createdTask.Id,
                OldStatus = TaskStatus.Pending,
                NewStatus = TaskStatus.Pending,
                ChangedAt = createdTask.CreatedAt,
                ChangedByUserId = createdTask.UserId
            },
            cancellationToken);

        return MapToResponseDto(createdTask);
    }

    public async Task<TaskResponseDto?> UpdateAsync(
        int id,
        UpdateTaskDto updateTaskDto,
        CancellationToken cancellationToken)
    {

        var duplicated = await _taskRepository.ExistsByTitleAndUserIdAsync(
                updateTaskDto.Title,
                updateTaskDto.UserId,
                id,
                cancellationToken);

        if (duplicated)
        {
            throw new BusinessException(
                "Ya existe una tarea con el mismo título para este usuario.");
        }

        var task = await _taskRepository.GetByIdAsync(id, cancellationToken);

        if (task is null)
        {
            return null;
        }

        var user = await _userRepository.GetByIdAsync(updateTaskDto.UserId, cancellationToken);

        if (user is null)
        {
            return null;
        }

        var previousStatus = task.Status;

        task.Title = updateTaskDto.Title;
        task.Description = updateTaskDto.Description;
        task.Priority = updateTaskDto.Priority;
        task.Status = updateTaskDto.Status;
        task.StartDate = updateTaskDto.StartDate;
        task.DueDate = updateTaskDto.DueDate;
        task.UserId = updateTaskDto.UserId;

        await _taskRepository.UpdateAsync(task, cancellationToken);

        if (previousStatus != task.Status)
        {
            await _taskStatusHistoryRepository.CreateAsync(
                new()
                {
                    TaskId = task.Id,
                    OldStatus = previousStatus,
                    NewStatus = task.Status,
                    ChangedAt = DateTime.UtcNow,
                    ChangedByUserId = task.UserId
                },
                cancellationToken);
        }

        return MapToResponseDto(task);
    }

    public Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        return _taskRepository.DeleteAsync(id, cancellationToken);
    }

    private static TaskResponseDto MapToResponseDto(TaskEntity task)
    {
        return new TaskResponseDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Priority = task.Priority,
            CreatedAt = task.CreatedAt,
            StartDate = task.StartDate,
            CompletedAt = task.CompletedAt,
            DueDate = task.DueDate,
            Status = task.Status,
            UserId = task.UserId
        };
    }

    public async Task<PagedResultDto<TaskResponseDto>> GetAllAsync(
     TaskFilterDto filter,
     CancellationToken cancellationToken)
    {
        var page = filter.Page < 1 ? 1 : filter.Page;

        var pageSize = filter.PageSize < 1
            ? 20
            : Math.Min(filter.PageSize, 100);

        filter.Page = page;
        filter.PageSize = pageSize;

        var result = await _taskRepository.GetPagedAsync(
            filter,
            cancellationToken);

        var totalPages = (int)Math.Ceiling(
            result.TotalItems / (double)pageSize);

        return new PagedResultDto<TaskResponseDto>
        {
            Items = result.Items.Select(MapToResponseDto),
            Page = page,
            PageSize = pageSize,
            TotalItems = result.TotalItems,
            TotalPages = totalPages
        };
    }

    Task<bool> ITaskService.DeleteAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
