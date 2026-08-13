using TaskManager.Application.Interfaces;
using UserEntity = TaskManager.Domain.Entities.User;

namespace TaskManager.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<UserEntity?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return _userRepository.GetByIdAsync(id, cancellationToken);
    }

    public Task<IEnumerable<UserEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return _userRepository.GetAllAsync(cancellationToken);
    }

    public Task<UserEntity> CreateAsync(UserEntity user, CancellationToken cancellationToken)
    {
        return _userRepository.CreateAsync(user, cancellationToken);
    }

    public Task UpdateAsync(UserEntity user, CancellationToken cancellationToken)
    {
        return _userRepository.UpdateAsync(user, cancellationToken);
    }
}
