using TaskManager.Application.DTOs;
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

    public Task<UserEntity> CreateAsync(CreateUserDto createUserDto, CancellationToken cancellationToken)
    {
        var user = new UserEntity
        {
            Name = createUserDto.Name,
            Email = createUserDto.Email
        };

        return _userRepository.CreateAsync(user, cancellationToken);
    }

    public async Task UpdateAsync(
        int id,
        UpdateUserDto updateUserDto,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(id, cancellationToken);

        if (user is null)
        {
            return;
        }

        user.Name = updateUserDto.Name;
        user.Email = updateUserDto.Email;

        await _userRepository.UpdateAsync(user, cancellationToken);
    }
}
