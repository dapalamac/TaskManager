using Microsoft.EntityFrameworkCore;
using TaskManager.Application.DTOs;
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

    public async Task DeleteAsync(
    int id,
    CancellationToken cancellationToken)
    {
        var task = await _context.Tasks.FindAsync(
            [id],
            cancellationToken);

        if (task is not null)
        {
            var histories = await _context.TaskStatusHistories
                .Where(h => h.TaskId == id)
                .ToListAsync(cancellationToken);

            _context.TaskStatusHistories.RemoveRange(histories);

            _context.Tasks.Remove(task);

            await _context.SaveChangesAsync(
                cancellationToken);
        }
    }

    public async Task<(IReadOnlyList<TaskEntity> Items, int TotalItems)> GetPagedAsync(
    TaskFilterDto filter,
    CancellationToken cancellationToken)
    {
        var query = _context.Tasks
            .AsNoTracking()
            .AsQueryable();

        if (filter.Priority.HasValue)
        {
            query = query.Where(t =>
                t.Priority == filter.Priority.Value);
        }

        if (filter.Status.HasValue)
        {
            query = query.Where(t =>
                t.Status == filter.Status.Value);
        }

        if (filter.UserId.HasValue)
        {
            query = query.Where(t =>
                t.UserId == filter.UserId.Value);
        }

        if (filter.StartDate.HasValue)
        {
            query = query.Where(t =>
                t.StartDate >= filter.StartDate.Value);
        }

        if (filter.EndDate.HasValue)
        {
            query = query.Where(t =>
                t.DueDate <= filter.EndDate.Value);
        }

        var totalItems = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderBy(t => t.Id)
            .Skip((filter.Page - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync(cancellationToken);

        return (items, totalItems);
    }

    public async Task<bool> ExistsByTitleAndUserIdAsync(
    string title,
    int userId,
    int? excludeTaskId,
    CancellationToken cancellationToken)
    {
        var query = _context.Tasks
            .AsNoTracking()
            .Where(t =>
                t.Title == title &&
                t.UserId == userId);

        if (excludeTaskId.HasValue)
        {
            query = query.Where(t =>
                t.Id != excludeTaskId.Value);
        }

        return await query.AnyAsync(cancellationToken);
    }

    public IQueryable<TaskEntity> GetQueryable()
    {
        return _context.Tasks
            .AsNoTracking();
    }
}
