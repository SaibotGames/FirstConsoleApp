using DTOs;

namespace BlazorApp.Services;

public interface IUserService
{
    Task<CreateUserDto> AddAsync(CreateUserDto userToAdd);
    Task UpdateAsync(UpdateUserDto userToUpdate, int id);
    Task DeleteAsync(int id);
    Task<UserDto> GetSingleAsync(int id);
    IQueryable<UserDto> GetMany();
    Task<UserDto> GetSingleByUsernameAsync(string username);
}