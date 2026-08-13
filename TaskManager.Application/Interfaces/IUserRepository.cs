using UserEntity = TaskManager.Domain.Entities.User;

namespace TaskManager.Application.Interfaces;

public interface IUserRepository
{
    Task<UserEntity?> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task<IEnumerable<UserEntity>> GetAllAsync(CancellationToken cancellationToken);

    Task<UserEntity> CreateAsync(UserEntity user, CancellationToken cancellationToken);

    Task UpdateAsync(UserEntity user, CancellationToken cancellationToken);
}
