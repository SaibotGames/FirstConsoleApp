using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class PostFileRepository : IPostRepository
{
    private readonly string _filePath = "posts.json";

    public PostFileRepository()
    {
        if (!File.Exists(_filePath))
        {
            File.WriteAllText(_filePath, "[]");
        }
    }


    public async Task<Post> AddAsync(Post post)
    {
        string postsAsJson = await File.ReadAllTextAsync(_filePath);
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsJson) !;
        int maxId = posts.Count > 0 ? posts.Max(x => x.Id) : 1;
        post.Id = maxId + 1;
        posts.Add(post);
        postsAsJson = JsonSerializer.Serialize(posts);
        await File.WriteAllTextAsync(_filePath, postsAsJson);
        return post;
    }

    public async Task UpdateAsync(Post post)
    {
       string postsAsJson = await File.ReadAllTextAsync(_filePath);
       List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsJson) !;
       Post? postToUpdate = posts.SingleOrDefault(x => x.Id == post.Id);
       if (postToUpdate is null)
       {
           throw new NotFoundException($"Post with ID '{post.Id}' not found");
       }
       posts.Remove(postToUpdate);
       posts.Add(post);
       postsAsJson = JsonSerializer.Serialize(posts);
       await File.WriteAllTextAsync(_filePath, postsAsJson);
    }

    public async Task DeleteAsync(int id)
    {
        string postsAsJson = await File.ReadAllTextAsync(_filePath);
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsJson) !;
        Post? postToDelete = posts.SingleOrDefault(x => x.Id == id);
        if (postToDelete is null)
        {
            throw new NotFoundException($"Post with ID '{id}' not found");
        }
        posts.Remove(postToDelete);
        postsAsJson = JsonSerializer.Serialize(posts);
        await File.WriteAllTextAsync(_filePath, postsAsJson);
    }

    public async Task<Post> GetSingleAsync(int id)
    {
        string postsAsJson = await File.ReadAllTextAsync(_filePath);
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsJson) !;
        Post? postToGet = posts.SingleOrDefault(x => x.Id == id);
        if (postToGet is null)
        {
            throw new NotFoundException($"Post with ID '{id}' not found");
        }
        return postToGet;
    }

    public IQueryable<Post> GetMany()
    {
        string postsAsJson = File.ReadAllTextAsync(_filePath).Result;
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsJson) !;
        return posts.AsQueryable();
    }
}