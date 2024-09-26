using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class CommentFileRepository : ICommentRepository
{
    private readonly string _filePath = "comments.json";

    public CommentFileRepository()
    {
        if (!File.Exists(_filePath))
        {
            File.WriteAllText(_filePath, "[]");
        }
    }
    

    public async Task<Comment> AddAsync(Comment comment)
    {
        string commentAsJson = await File.ReadAllTextAsync(_filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentAsJson) !;
        int maxId = comments.Count > 0 ? comments.Max(c => c.Id) : 1;
        comment.Id = maxId + 1;
        comments.Add(comment);
        commentAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(_filePath, commentAsJson);
        return comment;
    }

    public async Task UpdateAsync(Comment comment)
    {
        string commentAsJson = await File.ReadAllTextAsync(_filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentAsJson) !;
        Comment ? commentToUpdate = comments.SingleOrDefault(c => c.Id == comment.Id);
        if (commentToUpdate is null)
        {
            throw new NotFoundException($"Comment with ID '{comment.Id}' not found");
        }
        comments.Remove(commentToUpdate);
        comments.Add(comment);
        commentAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(_filePath, commentAsJson);
    }

    public async Task DeleteAsync(int id)
    {
        string commentsAsJson = await File.ReadAllTextAsync(_filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson) !;
        Comment? commentToDelete = comments.SingleOrDefault(c => c.Id == id);
        if (commentToDelete is null)
        {
            throw new NotFoundException(
                $"Comment with ID '{id}' not found");
        }
        comments.Remove(commentToDelete);
        commentsAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(_filePath, commentsAsJson);
    }

    public async Task<Comment> GetSingleAsync(int id)
    {
        string commentsAsJson = await File.ReadAllTextAsync(_filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson) !;
        Comment? comment = comments.SingleOrDefault(c => c.Id == id);
        if (comment is null)
        {
            throw new NotFoundException(
                $"Comment with ID '{id}' not found");
        }
        return comment;
    }

    public IQueryable<Comment> GetMany()
    {
        string commentsAsJson = File.ReadAllTextAsync(_filePath).Result;
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson) !;
        return comments.AsQueryable();
    }
    
}