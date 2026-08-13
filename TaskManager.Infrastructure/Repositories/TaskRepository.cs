using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Interfaces;
using TaskManager.Infrastructure.Data;
using TaskEntity = TaskManager.Domain.Entities.Task;

namespace TaskManager.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly TaskManagerDbContext _context;

    public TaskRepository(TaskManagerDbContext context)
    {
        _context = context;
    }

    public async Task<TaskEntity?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Tasks
            .AsNoTracking()
            .FirstOrDefaultAsync(task => task.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<TaskEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Tasks
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<TaskEntity> CreateAsync(TaskEntity task, CancellationToken cancellationToken)
    {
        await _context.Tasks.AddAsync(task, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return task;
    }

    public async Task UpdateAsync(TaskEntity task, CancellationToken cancellationToken)
    {
        _context.Tasks.Update(task);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var task = await _context.Tasks.FindAsync([id], cancellationToken);

        if (task is not null)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
