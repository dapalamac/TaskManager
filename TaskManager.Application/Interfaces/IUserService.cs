using TaskManager.Application.DTOs;
using UserEntity = TaskManager.Domain.Entities.User;

namespace TaskManager.Application.Interfaces;

public interface IUserService
{
    Task<UserEntity?> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task<IEnumerable<UserEntity>> GetAllAsync(CancellationToken cancellationToken);

    Task<UserEntity> CreateAsync(CreateUserDto createUserDto, CancellationToken cancellationToken);

    Task UpdateAsync(int id, UpdateUserDto updateUserDto, CancellationToken cancellationToken);
}
