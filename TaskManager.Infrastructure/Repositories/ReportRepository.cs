using Microsoft.EntityFrameworkCore;
using TaskManager.Application.DTOs;
using TaskManager.Application.Interfaces;
using TaskManager.Infrastructure.Data;

namespace TaskManager.Infrastructure.Repositories;

public class ReportRepository : IReportRepository
{
    private readonly TaskManagerDbContext _context;

    public ReportRepository(TaskManagerDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PendingTasksReportDto>> GetPendingTasksAsync(
        CancellationToken cancellationToken)
    {
        return await _context.Database
            .SqlQuery<PendingTasksReportDto>(
                $"EXEC dbo.sp_GetPendingTasks")
            .ToListAsync(cancellationToken);
    }
}