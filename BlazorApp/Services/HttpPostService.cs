using DTOs;
using System.Net.Http.Json;

namespace BlazorApp.Services;

public class HttpPostService : IPostService
{
    private readonly HttpClient client;

    public HttpPostService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<CreatePostDto> AddPostAsync(CreatePostDto post)
    {
        // Await the response to ensure the request completes
        var response = await client.PostAsJsonAsync("api/Posts", post);
        response.EnsureSuccessStatusCode(); // Optional: ensures a successful response
        return post;
    }
    
    public async Task UpdateAsync(UpdatePostDto post, int id)
    {
        // Await the response to ensure the request completes
        var response = await client.PutAsJsonAsync($"api/Posts/{id}", post);
        response.EnsureSuccessStatusCode(); // Optional: ensures a successful response
    }

    public async Task DeletePostAsync(int id)
    {
        // Await the delete request to ensure it completes
        var response = await client.DeleteAsync($"api/Posts/{id}");
        response.EnsureSuccessStatusCode(); // Optional: ensures a successful response
    }

    public async Task<PostDto> GetSinglePostAsync(int id)
    {
        // Await the Get request to ensure it completes
        var post = await client.GetFromJsonAsync<PostDto>($"api/Posts/{id}");
        return post!;
    }

    public async Task<List<PostDto>> GetManyPosts()
    {
        // Await the Get request to ensure it completes
        var posts = await client.GetFromJsonAsync<List<PostDto>>("api/Posts");
        return posts!;
    }

    public async Task<CreateCommentDto> AddCommentAsync(CreateCommentDto comment, int postId)
    {
        // Await the Post request to ensure the comment is added
        var response = await client.PostAsJsonAsync($"api/Posts/{postId}/comments", comment);
        response.EnsureSuccessStatusCode(); // Optional: ensures a successful response
        return comment;
    }

    public async Task UpdateCommentAsync(UpdateCommentDto comment, int postId, int commentId)
    {
        // Await the Put request to ensure the comment is updated
        var response = await client.PutAsJsonAsync($"api/Posts/{postId}/comments/{commentId}", comment);
        response.EnsureSuccessStatusCode(); // Optional: ensures a successful response
    }

    public async Task DeleteCommentAsync(int postId, int commentId)
    {
        // Await the delete request to ensure it completes
        var response = await client.DeleteAsync($"api/Posts/{postId}/comments/{commentId}");
        response.EnsureSuccessStatusCode(); // Optional: ensures a successful response
    }

    public async Task<CommentDto> GetSingleCommentAsync(int postId, int commentId)
    {
        // Await the Get request to ensure it completes
        var comment = await client.GetFromJsonAsync<CommentDto>($"api/Posts/{postId}/comments/{commentId}");
        return comment!;
    }

    public async Task<List<CommentDto>> GetManyComments(int postId)
    {
        // Await the Get request to ensure it completes
        var comments = await client.GetFromJsonAsync<List<CommentDto>>($"api/Posts/{postId}/comments");
        return comments!;
    }
}
