using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.DTOs;
using TaskManager.Application.Interfaces;

namespace TaskManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResultDto<TaskResponseDto>>> GetAll(
        [FromQuery] TaskFilterDto filter,
        CancellationToken cancellationToken)
    {
        var result = await _taskService.GetAllAsync(
            filter,
            cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TaskResponseDto>> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var task = await _taskService.GetByIdAsync(
            id,
            cancellationToken);

        return task is null
            ? NotFound()
            : Ok(task);
    }

    [HttpPost]
    public async Task<ActionResult<TaskResponseDto>> Create(
        CreateTaskDto createTaskDto,
        CancellationToken cancellationToken)
    {
        var task = await _taskService.CreateAsync(
            createTaskDto,
            cancellationToken);

        if (task is null)
        {
            return BadRequest();
        }

        return CreatedAtAction(
            nameof(GetById),
            new { id = task.Id },
            task);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<TaskResponseDto>> Update(
        int id,
        UpdateTaskDto updateTaskDto,
        CancellationToken cancellationToken)
    {
        var task = await _taskService.UpdateAsync(
            id,
            updateTaskDto,
            cancellationToken);

        return task is null
            ? NotFound()
            : Ok(task);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int id,
        CancellationToken cancellationToken)
    {
        var deleted = await _taskService.DeleteAsync(
            id,
            cancellationToken);

        return deleted
            ? NoContent()
            : NotFound();
    }
}