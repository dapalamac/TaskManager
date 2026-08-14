using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.DTOs;
using TaskManager.Application.Interfaces;

namespace TaskManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskStatusHistoryController : ControllerBase
{
    private readonly ITaskStatusHistoryService _taskStatusHistoryService;

    public TaskStatusHistoryController(ITaskStatusHistoryService taskStatusHistoryService)
    {
        _taskStatusHistoryService = taskStatusHistoryService;
    }

    [HttpGet("task/{taskId:int}")]
    public async Task<ActionResult<IEnumerable<TaskStatusHistoryResponseDto>>> GetByTaskId(
        int taskId,
        CancellationToken cancellationToken)
    {
        var history = await _taskStatusHistoryService.GetByTaskIdAsync(taskId, cancellationToken);

        return history.Any() ? Ok(history) : NotFound();
    }
}
