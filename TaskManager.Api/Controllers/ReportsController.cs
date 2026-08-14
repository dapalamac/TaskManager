using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.DTOs;
using TaskManager.Application.Interfaces;

namespace TaskManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportsController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet("pending-tasks")]
    public async Task<ActionResult<IEnumerable<PendingTasksReportDto>>> GetPendingTasks(
        CancellationToken cancellationToken)
    {
        var report = await _reportService.GetPendingTasksAsync(
            cancellationToken);

        return Ok(report);
    }
}