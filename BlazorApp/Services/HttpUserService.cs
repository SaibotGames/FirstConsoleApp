using DTOs;

namespace BlazorApp.Services;

public class HttpUserService : IUserService
{
    private readonly HttpClient client;

    public HttpUserService(HttpClient client)
    {
        this.client = client;
        
    }

    public async Task<CreateUserDto> AddAsync(CreateUserDto userToAdd)
    {
        try
        {
            var newUser = await client.PostAsJsonAsync("api/Users", userToAdd);
            if (!newUser.IsSuccessStatusCode)
            {
                string errorMessage = await newUser.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Failed to add user: {errorMessage}");
            }
            return userToAdd;
        }
        catch (Exception e)
        {
            throw new HttpRequestException(e.Message);
        }
    }

    public async Task UpdateAsync(UpdateUserDto userToUpdate, int id)
    {
        try
        {
            var update = await client.PutAsJsonAsync($"api/Users/{id}", userToUpdate);
            if (!update.IsSuccessStatusCode)
            {
                string errorMessage = await update.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Failed to update user: {errorMessage}");
            }

        }
        catch (Exception e)
        {
            throw new HttpRequestException(e.Message);
        }
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            var delete = await client.DeleteAsync($"api/Users/{id}");
            if (!delete.IsSuccessStatusCode)
            {
                string errorMessage = await delete.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Failed to delete user: {errorMessage}");
            }
        }
        catch (Exception e)
        {
            throw new HttpRequestException(e.Message);
        }
    }

    public async Task<UserDto> GetSingleAsync(int id)
    {
        try
        {
            var user = await client.GetFromJsonAsync<UserDto>($"api/Users/{id}");
            if (user is null)
            {
                string errorMessage = "User not found";
                throw new HttpRequestException($"Failed to fetch user: {errorMessage}");
            }
            return user;
        }
        catch (Exception e)
        {
            throw new HttpRequestException(e.Message);
        }
    }

    public IQueryable<UserDto> GetMany()
    {
        throw new NotImplementedException();
    }

    public async Task<UserDto> GetSingleByUsernameAsync(string username)
    {
        try
        {
            var user = await client.GetFromJsonAsync<UserDto>($"api/Users/{username}");
            if (user is null)
            {
                string errorMessage = "User not found";
                throw new HttpRequestException($"Failed to fetch user: {errorMessage}");
            }
            return user;
        }
        catch (Exception e)
        {
            throw new HttpRequestException(e.Message);
        }
    }
}