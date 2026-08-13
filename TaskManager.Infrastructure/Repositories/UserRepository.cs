using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Interfaces;
using TaskManager.Infrastructure.Data;
using UserEntity = TaskManager.Domain.Entities.User;

namespace TaskManager.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly TaskManagerDbContext _context;

    public UserRepository(TaskManagerDbContext context)
    {
        _context = context;
    }

    public async Task<UserEntity?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<UserEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Users
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<UserEntity> CreateAsync(UserEntity user, CancellationToken cancellationToken)
    {
        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return user;
    }

    public async Task UpdateAsync(UserEntity user, CancellationToken cancellationToken)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
