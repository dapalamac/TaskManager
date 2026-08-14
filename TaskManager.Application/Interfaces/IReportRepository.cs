using TaskManager.Application.DTOs;

namespace TaskManager.Application.Interfaces;

public interface IReportRepository
{
    Task<IEnumerable<PendingTasksReportDto>> GetPendingTasksAsync(
        CancellationToken cancellationToken);
}