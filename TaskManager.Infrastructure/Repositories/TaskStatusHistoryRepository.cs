using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Interfaces;
using TaskManager.Infrastructure.Data;
using TaskStatusHistoryEntity = TaskManager.Domain.Entities.TaskStatusHistory;

namespace TaskManager.Infrastructure.Repositories;

public class TaskStatusHistoryRepository : ITaskStatusHistoryRepository
{
    private readonly TaskManagerDbContext _context;

    public TaskStatusHistoryRepository(TaskManagerDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TaskStatusHistoryEntity>> GetByTaskIdAsync(
        int taskId,
        CancellationToken cancellationToken)
    {
        return await _context.TaskStatusHistories
            .AsNoTracking()
            .Where(history => history.TaskId == taskId)
            .ToListAsync(cancellationToken);
    }

    public async Task<TaskStatusHistoryEntity> CreateAsync(
        TaskStatusHistoryEntity taskStatusHistory,
        CancellationToken cancellationToken)
    {
        await _context.TaskStatusHistories.AddAsync(taskStatusHistory, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return taskStatusHistory;
    }
}
