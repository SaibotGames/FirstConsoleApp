using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class UserFileRepository : IUserRepository
{
    private readonly string _filePath = "users.json";

    public UserFileRepository()
    {
        if (!File.Exists(_filePath))
        {
           File.WriteAllText(_filePath, "[]"); 
        }
    }

    public async Task<User> AddAsync(User user)
    {
        string usersAsJson = await File.ReadAllTextAsync(_filePath);
        List<User> users =
            JsonSerializer.Deserialize<List<User>>(usersAsJson) !;
        int maxId = users.Count > 0 ? users.Max(x => x.Id) : 1;
        user.Id = maxId + 1;
        users.Add(user);
        usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(_filePath, usersAsJson);
        return user;
    }

    public async Task UpdateAsync(User user)
    {
        string usersAsJson = await File.ReadAllTextAsync(_filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson) !;
        User? userToUpdate = users.SingleOrDefault(x => x.Id == user.Id);
        if (userToUpdate is null)
        {
            throw new NotFoundException($"User with ID '{user.Id}' not found");
        }
        users.Remove(userToUpdate);
        users.Add(user);
        usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(_filePath, usersAsJson);
    }

    public async Task DeleteAsync(int id)
    {
        string usersAsJson = await File.ReadAllTextAsync(_filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson) !;
        User? userToDelete = users.SingleOrDefault(x => x.Id == id);
        if (userToDelete is null)
        {
            throw new NotFoundException($"User with ID '{id}' not found");
        }
        users.Remove(userToDelete);
        usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(_filePath, usersAsJson);
    }

    public async Task<User> GetSingleAsync(int id)
    {
        string usersAsJson = await File.ReadAllTextAsync(_filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson) !;
        User? user = users.SingleOrDefault(x => x.Id == id);
        if (user is null)
        {
            throw new NotFoundException($"User with ID '{id}' not found");
        }
        return user;
    }

    public IQueryable<User> GetMany()
    {
        string usersAsJson = File.ReadAllTextAsync(_filePath).Result;
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson) !;
        return users.AsQueryable();
    }
}