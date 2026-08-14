using TaskManager.Application.DTOs;

namespace TaskManager.Application.Interfaces;

public interface IReportService
{
    Task<IEnumerable<PendingTasksReportDto>> GetPendingTasksAsync(
        CancellationToken cancellationToken);
}