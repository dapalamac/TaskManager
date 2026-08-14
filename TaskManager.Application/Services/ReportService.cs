using TaskManager.Application.DTOs;
using TaskManager.Application.Interfaces;

namespace TaskManager.Application.Services;

public class ReportService : IReportService
{
    private readonly IReportRepository _reportRepository;

    public ReportService(IReportRepository reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<IEnumerable<PendingTasksReportDto>> GetPendingTasksAsync(
        CancellationToken cancellationToken)
    {
        return await _reportRepository.GetPendingTasksAsync(
            cancellationToken);
    }
}