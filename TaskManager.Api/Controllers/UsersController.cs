using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.DTOs;
using TaskManager.Application.Interfaces;
using UserEntity = TaskManager.Domain.Entities.User;

namespace TaskManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserEntity>>> GetAll(CancellationToken cancellationToken)
    {
        var users = await _userService.GetAllAsync(cancellationToken);

        return Ok(users);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserEntity>> GetById(int id, CancellationToken cancellationToken)
    {
        var user = await _userService.GetByIdAsync(id, cancellationToken);

        return user is null ? NotFound() : Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<UserEntity>> Create(
        CreateUserDto createUserDto,
        CancellationToken cancellationToken)
    {
        var createdUser = await _userService.CreateAsync(createUserDto, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUser);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(
        int id,
        UpdateUserDto updateUserDto,
        CancellationToken cancellationToken)
    {
        var existingUser = await _userService.GetByIdAsync(id, cancellationToken);

        if (existingUser is null)
        {
            return NotFound();
        }

        await _userService.UpdateAsync(id, updateUserDto, cancellationToken);

        return Ok();
    }
}
