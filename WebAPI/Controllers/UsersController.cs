namespace WebAPI.Controllers;

using DTOs;
using Entities;
using RepositoryContracts;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase{
    private readonly IUserRepository userRepository;

    public UsersController(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
    {
        var newUser = new User(createUserDto.UserName, createUserDto.Password);

        try
        {
            var addedUser = await userRepository.AddAsync(newUser);
            var userDto = MapToUserDto(addedUser);

            return CreatedAtAction(nameof(GetUser), new { id = addedUser.Id }, userDto);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto updateUserDto)
    {
        var existingUser = await userRepository.GetSingleAsync(id);
        existingUser.UserName = updateUserDto.UserName ?? existingUser.UserName;
        existingUser.Password = updateUserDto.Password ?? existingUser.Password;

        await userRepository.UpdateAsync(existingUser);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await userRepository.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await userRepository.GetSingleAsync(id);
        var userDto = MapToUserDto(user);
        return Ok(userDto);
    }

    [HttpGet]
    public IActionResult GetAllUsers()
    {
        var users = userRepository.GetMany().Select(MapToUserDto).ToList();
        return Ok(users);
    }

    private static UserDto MapToUserDto(User user)
    {
        return new UserDto
        {
            Id = user.Id,
            UserName = user.UserName,
        };
    }

    [HttpGet("username/{username}")]
    public async Task<IActionResult> GetUserByUsername(string username)
    {
        var user = await userRepository.GetSingleByUsernameAsync(username);
        var userDto = MapToUserDto(user);
        return Ok(userDto);
    }
}