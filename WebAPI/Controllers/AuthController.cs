using DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository repository;

    public AuthController(IUserRepository repository)
    {
        this.repository = repository;
    }

    [HttpPost("login")]
    public async Task<IResult> Login([FromBody] MyLoginRequest request)
    {
        try
        {
            var user = await repository.GetSingleByUsernameAsync(request.Username);

            if (user.Password != request.Password)
            {
                return Results.Unauthorized();
            }

            var userDto = new UserDto
            {
                Id = user.Id,
                UserName = user.UserName
            };

            return Results.Ok(userDto);
        }
        catch (NotFoundException e)
        {
            
            return Results.Unauthorized();
        }
        catch (Exception e)
        {
            return Results.Problem("An error occurred", statusCode: 500);
        }
    }
}